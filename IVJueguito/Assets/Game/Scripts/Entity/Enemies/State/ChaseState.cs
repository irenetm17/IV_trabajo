using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Chase")]
public class ChaseState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        Vector3 playerPos = enemy.SearchPlayer();

        enemy.MoveTo(playerPos);

        if (enemy.DistanceWithPlayer() < 1)
        {
            enemy.ChangeState(enemy.flyweightData.attackState);
        }

        if (enemy.DistanceWithPlayer() > 5)
        {
            enemy.ChangeState(enemy.flyweightData.idleState);
        }

    }
    public override void Exit(Enemy enemy)
    {
        
    }

}
