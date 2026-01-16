using UnityEngine;

[CreateAssetMenu(menuName = "States/ChaseBoss")]
public class ChaseBossState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        enemy.animator.SetBool("nearPlayer", false);

        Debug.Log($"<color=cyan>{enemy.name}</color> ha entrado en el estado: <color=yellow>{this.name}</color>");

        MiniBoss boss = enemy as MiniBoss;
        if (boss == null) return;

        boss.triggerSpecial = false;
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        MiniBoss boss = enemy as MiniBoss;

        Vector3 playerPos = boss.SearchPlayer();

        boss.MoveTo(playerPos);

        if (boss.DistanceWithPlayer() < enemy.flyweightData.reachPlayerRadius)
        {
            boss.ChangeState(boss.flyweightData.attackState);
        }

        if (boss.DistanceWithPlayer() > enemy.flyweightData.detectPlayerRadius)
        {
            boss.ChangeState(boss.flyweightData.idleState);
        }

    }
    public override void Exit(Enemy enemy)
    {

    }
}
