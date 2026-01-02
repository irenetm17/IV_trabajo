using UnityEngine;

[CreateAssetMenu(fileName = "MiniBossFlyweightData", menuName = "Enemy/FlyweightData")]

public class MiniBossFlyweight : EnemyFlyweight
{
    [Header("Comportamientos de Jefe")]
    public EnemyState specialGimmickBossState;
    public EnemyState specialAtatckBossState;
    public EnemyState stuntBossState;
}
