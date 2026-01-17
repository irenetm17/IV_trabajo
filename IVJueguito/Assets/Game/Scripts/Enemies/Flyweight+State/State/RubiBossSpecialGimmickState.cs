using UnityEngine;

[CreateAssetMenu(menuName = "States/SpecialGimmick")]
public class RubiBossSpecialGimmickState : EnemyState
{
    [SerializeField] float chargeTime = 5f;
    [SerializeField] float windRadius = 30f;
    [SerializeField] float windForce = 2000f;
    public override void Enter(Enemy enemy)
    {
        Debug.Log($"<color=cyan>{enemy.name}</color> ha entrado en el estado: <color=yellow>{this.name}</color>");

        enemy.StopMoving();
        enemy.stateTimer = 0;
        enemy.tookDamage = false;
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        MiniBoss boss = enemy as MiniBoss;
        if (boss == null) return;

        enemy.stateTimer += deltaTime;

        boss.pushPlayer(windForce, windRadius);

        if (enemy.stateTimer >= chargeTime)
        {
            boss.ChangeState(boss.MiniBossData.specialAttackBossState);
        }

        if (boss.tookDamage)
        {
            boss.ChangeState(boss.MiniBossData.stunBossState);
        }
    }
    public override void Exit(Enemy enemy)
    {
        
    }
}
