using UnityEngine;

[CreateAssetMenu(menuName = "States/Stun")]
public class StunBossState : EnemyState
{
    private float stunnedTime = 5f;
    public override void Enter(Enemy enemy)
    {
        MiniBoss boss = enemy as MiniBoss;
        if (boss == null) return;

        Debug.Log($"<color=cyan>{enemy.name}</color> ha entrado en el estado: <color=yellow>{this.name}</color>");

        enemy.stateTimer = 0;
        enemy.StopMoving();
        boss.StopWind();
        boss.StunEffect();
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        MiniBoss boss = enemy as MiniBoss;
        if (boss == null) return;

        enemy.stateTimer += deltaTime;

        if (enemy.stateTimer > stunnedTime)
        {
            enemy.ChangeState(boss.MiniBossData.idleState);
        }
        
    }
    public override void Exit(Enemy enemy)
    {
        
    }
}
