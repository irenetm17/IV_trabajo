using UnityEngine;

[CreateAssetMenu(menuName = "States/Idle")]
public class IdleState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        Vector3 wayPoint = enemy.GetRandomWayPoint();
        
        enemy.MoveTo(wayPoint);

        Vector3 playerPos = enemy.SearchPlayer();

        if (enemy.DistanceWithPlayer() < 5)
        {
            enemy.ChangeState(enemy.flyweightData.chaseState);
        }
        
    }
    public override void Exit(Enemy enemy)
    {
        
    }
}
