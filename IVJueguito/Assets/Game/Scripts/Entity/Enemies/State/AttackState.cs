using UnityEngine;

[CreateAssetMenu(menuName = "States/Attack")]
public class AttackState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        enemy.animator.SetBool("nearPlayer", true);
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        enemy.DamageTarget(enemy.flyweightData.damage);

        if (enemy.DistanceWithPlayer() < 5f)
        {
            enemy.ChangeState(enemy.flyweightData.chaseState);
        }
    }
    public override void Exit(Enemy enemy)
    {
        enemy.animator.SetBool("nearPlayer", false);
    }
}
