using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Datos")]
    public int currentHp;
    public bool isAlive;
    public EnemyFlyweight flyweightData;
    public Vector3 pos;
    public Vector3 dir;

    private Vector3 spawnPosition;
    private float patrolRadius;
    public Animator animator;

    private StateMachine stateMachine;

    public void Initialize(EnemyType type)
    {
        flyweightData = EnemyFlyweightFactory.Instance.GetFlyweight(type);

        currentHp = flyweightData.maxHP;
        isAlive = true;
        patrolRadius = flyweightData.patrolRadius;

        if (flyweightData.animatorOverride != null && animator != null)
        {
            animator.runtimeAnimatorController = flyweightData.animatorOverride;
        }

        stateMachine = new StateMachine(); 
        stateMachine.Initialize(flyweightData.idleState, this);
    }
    void Update()
    {
        if (!isAlive) return;

        stateMachine.Update(this, Time.deltaTime);
    }
    public void ChangeState(EnemyState newState)
    {
        stateMachine.ChangeState(newState, this);
    }
    public bool IsAlive()
    {
        return isAlive;
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            isAlive = false;
            ChangeState(flyweightData.dieState);
        }

    }

    public void DamageTarget(int damageDealt)
    {
        
    }

    public void MoveTo(Vector3 target)
    {
        float step = flyweightData.speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void StopMoving()
    {
        
    }

    public Vector3 SearchPlayer()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        return playerPos;
    }

    public float DistanceWithPlayer()
    {
        Vector3 playerPos = this.SearchPlayer();

        return Vector3.Distance(transform.position, playerPos);
       
    }

    public Vector3 GetRandomWayPoint()
    {
    
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;

        // 2. Le sumamos el origen para mover esa esfera a la zona de juego
        // IMPORTANTE: Usar 'spawnPosition' para que siempre patrulle su zona.
        // Si usaras 'transform.position', la zona se iría moviendo con él (drift).
        randomDirection += spawnPosition;

        // 3. Encontramos el punto válido más cercano en el NavMesh
        NavMeshHit hit;

        // Parámetros: (Punto deseado, Resultado, Distancia máx de corrección, Capas)
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return spawnPosition;
    }

}
