using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFlyweightData", menuName = "Enemy/FlyweightData")]
public class EnemyFlyweight : ScriptableObject
{
    public EnemyType typeID;

    [Header("Stats Base")]
    public int maxHP;
    public float speed;
    public int damage;
    public float patrolRadius;
    public float detectPlayerRadius;
    public float reachPlayerRadius;

    [Header("Apariencia")]
    public AnimatorOverrideController animatorOverride;

    [Header("Comportamientos")]
    public EnemyState idleState;
    public EnemyState chaseState;
    public EnemyState attackState;
    public EnemyState dieState;

}
