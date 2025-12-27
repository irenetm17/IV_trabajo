using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFlyweightData", menuName = "Enemy/FlyweightData")]
public class EnemyFlyweight : ScriptableObject
{
    public EnemyType typeID;

    [Header("Stats Base")]
    public int maxHP;
    public float speed;
    public int damage;

    [Header("Apariencia")]
    public AnimatorOverrideController animatorOverride;

    [Header("Comportamientos")]
    public EnemyState idleState;
    public EnemyState chaseState;
    public EnemyState attackState;
    public EnemyState dieState;

}
