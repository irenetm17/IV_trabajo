using UnityEngine;

[CreateAssetMenu(menuName = "States/Idle")]
public class IdleState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        Debug.Log($"<color=cyan>{enemy.name}</color> ha entrado en el estado: <color=yellow>{this.name}</color>");

        enemy.hasWayPoint = false;
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        if (!enemy.hasWayPoint)
        {
            enemy.currentWayPoint = enemy.GetRandomWayPoint();
            enemy.hasWayPoint = true;
        }

        enemy.MoveTo(enemy.currentWayPoint);

        Vector3 enemyXZ = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
        Vector3 targetXZ = new Vector3(enemy.currentWayPoint.x, 0, enemy.currentWayPoint.z);

        float distancia = Vector3.Distance(enemyXZ, targetXZ);

        if (distancia < 1f)
        {
            enemy.hasWayPoint = false;
        }

        if (enemy.DistanceWithPlayer() < enemy.flyweightData.detectPlayerRadius)
        {
            enemy.ChangeState(enemy.flyweightData.chaseState);
        }
        
    }
    public override void Exit(Enemy enemy)
    {
        enemy.hasWayPoint = false;
    }
}
