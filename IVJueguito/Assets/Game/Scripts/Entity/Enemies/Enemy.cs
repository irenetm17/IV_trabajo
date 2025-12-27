using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Datos")]
    public int currentHp;
    public bool isAlive;
    [SerializeField] EnemyFlyweight flyweightData;
    public Vector3 pos;
    public Vector3 dir;

    public Animator animator;

    private StateMachine stateMachine;

    public void Initialize(EnemyType type)
    {
        flyweightData = EnemyFlyweightFactory.Instance.GetFlyweight(type);

        currentHp = flyweightData.maxHP;
        isAlive = true;

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

    public void MoveTo(Vector3 target)
    {
        float step = flyweightData.speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void StopMoving()
    {
        
    }




}
