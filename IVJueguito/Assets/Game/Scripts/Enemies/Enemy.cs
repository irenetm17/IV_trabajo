using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Enemy : Entity, IEnemy, IObserver
{
    [Header("Datos")]
    public float currentHp;
    public bool isAlive;
    public EnemyFlyweight flyweightData; // Datos compartidos por tipo de enemigo (Flyweight)

    private Vector3 spawnPosition;
    [SerializeField] float patrolRadius;
    [SerializeField] Slider hpBar;
    public Animator animator;

    protected Transform playerTransform;

    [SerializeField] protected EnemyType tipoParaTest;

    private StateMachine stateMachine;

    // Variables usadas por estados
    [HideInInspector] public Vector3 currentWayPoint;

    [HideInInspector] public bool hasWayPoint = false;
    [HideInInspector] public bool tookDamage = false;
    [HideInInspector] public bool hasAttacked = false;

    [HideInInspector] public float stateTimer;

    // ANimaciones Feedback
    [SerializeField] private ParticleSystem deathParticle;

    public void Initialize(EnemyType type)
    {
        flyweightData = EnemyFlyweightFactory.Instance.GetFlyweight(type); // Obtener datos compartidos del tipo de enemigo

        spawnPosition = transform.position;
        currentHp = flyweightData.maxHP;
        isAlive = true;
        patrolRadius = flyweightData.patrolRadius;

        if (animator != null && flyweightData.animatorController != null)
        {
            animator.runtimeAnimatorController = flyweightData.animatorController;
        }

        // Buscar jugador
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }

        if (hpBar == null)
        {
            hpBar = GetComponentInChildren<Slider>(true);
        }

        hpBar.maxValue = currentHp;
        // Crear máquina de estados e iniciar en Idle
        stateMachine = new StateMachine();
        stateMachine.Initialize(flyweightData.idleState, this);
    }


    

    void Start()
    {
        hpBar = GetComponentInChildren<Slider>(true);
        animator = GetComponentInChildren<Animator>();
        Initialize(tipoParaTest);
        EventManager.instance.Subscribir(eventType.DamageTaken, this);
    }



    void OnDestroy()
    {
        // Limpieza obligatoria
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.DamageTaken, this);
        }
    }

    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.DamageTaken)
        {
            DamageTakenEvent event2 = (DamageTakenEvent)evento;
            if (event2.Target == this.GetComponent<Enemy>())
            {
                TakeDamage(event2.Amount);
            }
        }
    }

    protected virtual void Update()
    {
        if (!isAlive) return;

        stateMachine.Update(this, Time.deltaTime);
        hpBar.value = currentHp;
    }
    public void ChangeState(EnemyState newState)
    {
        Debug.Log($"{name} cambió de estado a: {newState.name}"); 
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

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        tookDamage = true;
        //DamageTakenEvent evt = new DamageTakenEvent(this.gameObject);
        //EventManager.instance.Publicar(evt);

        if (currentHp <= 0)
        {
            if(deathParticle!=null)
                deathParticle.Play();
            StartCoroutine(DieAnimation());
        }

    }

    public void DamageTarget(float damageDealt)
    {
        PlayerStatsEvent vidasRestar = new PlayerStatsEvent(-damageDealt, 0);
        EventManager.instance.Publicar(vidasRestar);
    }

    public virtual void KillEnemy()
    {
        Destroy(gameObject);
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
        if (playerTransform != null)
        {
            return playerTransform.position;
        }
        return Vector3.zero;
    }

    public float DistanceWithPlayer()
    {
        if (playerTransform == null)
            return 9999f;

        return Vector3.Distance(transform.position, playerTransform.position);

    }

    public Vector3 GetRandomWayPoint() // Devuelve un punto aleatorio navegable para patrulla
    {

        Vector2 circlePoint = Random.insideUnitCircle * patrolRadius;

        Vector3 targetPoint = spawnPosition + new Vector3(circlePoint.x, 0, circlePoint.y);

        NavMeshHit hit;

        float searchHeight = 20f;

        if (NavMesh.SamplePosition(targetPoint, out hit, searchHeight, NavMesh.AllAreas))
        {
            return hit.position;
        }

        Debug.LogWarning("¡Fallo al encontrar suelo en el NavMesh!");
        return spawnPosition;
    }


    IEnumerator DieAnimation()
    {
        while (transform.localScale.x > 0f)
        {
            Vector3 scale = transform.localScale;
            scale.x -= 0.1f;
            transform.localScale = scale;
            yield return new WaitForSeconds(0.2f);
        }
        Debug.Log("Murio");
        isAlive = false;
        ChangeState(flyweightData.dieState);
    }


}
