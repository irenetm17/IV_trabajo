using UnityEngine;

[CreateAssetMenu(fileName = "MiniBossFlyweightData", menuName = "Enemy/MiniBossFlyweightData")]

public class MiniBossFlyweight : EnemyFlyweight
{
    [Header("Comportamientos de Jefe")]
    public EnemyState specialGimmickBossState;
    public EnemyState specialAttackBossState;
    public EnemyState stunBossState;
}
