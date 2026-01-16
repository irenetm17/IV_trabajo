using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFlyweightData", menuName = "Enemy/FlyweightData")]
public class EnemyFlyweight : ScriptableObject
{
    public EnemyType typeID;

    [Header("Stats Base")]
    public int maxHP;
    public float speed;
    public float damage;
    public float patrolRadius;
    public float detectPlayerRadius;
    public float reachPlayerRadius;

    [Header("Apariencia")]
    public RuntimeAnimatorController animatorController;

    [Header("Estados")]
    public EnemyState idleState;
    public EnemyState chaseState;
    public EnemyState attackState;
    public EnemyState dieState;

}
