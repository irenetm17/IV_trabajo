using UnityEngine;

[CreateAssetMenu(menuName = "States/Stun")]
public class stunBossState : EnemyState
{
    private float stunnedTime = 3f;
    public override void Enter(Enemy enemy)
    {
        Debug.Log($"<color=cyan>{enemy.name}</color> ha entrado en el estado: <color=yellow>{this.name}</color>");

        enemy.stateTimer = Time.time;
        enemy.StopMoving();
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        MiniBoss boss = enemy as MiniBoss;
        if (boss == null) return;

        float timeElapsed = Time.time - boss.stateTimer;

        if (timeElapsed > stunnedTime)
        {
            enemy.ChangeState(boss.MiniBossData.idleState);
        }
        
    }
    public override void Exit(Enemy enemy)
    {
        
    }
}
