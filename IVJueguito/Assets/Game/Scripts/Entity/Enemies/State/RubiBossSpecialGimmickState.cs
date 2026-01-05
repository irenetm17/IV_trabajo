using UnityEngine;

[CreateAssetMenu(menuName = "States/SpecialGimmick")]
public class RubiBossSpecialGimmickState : EnemyState
{
    [SerializeField] float chargeTime = 5f;
    [SerializeField] float windRadius = 30f;
    [SerializeField] float windForce = 20f;
    public override void Enter(Enemy enemy)
    {
        Debug.Log($"<color=cyan>{enemy.name}</color> ha entrado en el estado: <color=yellow>{this.name}</color>");

        enemy.StopMoving();
        enemy.stateTimer = Time.time;
        enemy.tookDamage = false;
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        MiniBoss boss = enemy as MiniBoss;
        if (boss == null) return;

        float timeElapsed = Time.time - boss.stateTimer;
        float dist = enemy.DistanceWithPlayer();

        if(timeElapsed >= chargeTime)
        {
            boss.ChangeState(boss.MiniBossData.specialAttackBossState);
        }

        boss.pushPlayer(windForce, windRadius);
        

        if (boss.tookDamage)
        {
            boss.ChangeState(boss.MiniBossData.stunBossState);
        }
    }
    public override void Exit(Enemy enemy)
    {
        
    }
}
