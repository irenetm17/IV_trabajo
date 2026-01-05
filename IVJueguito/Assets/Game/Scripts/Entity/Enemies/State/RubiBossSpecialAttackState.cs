using UnityEngine;

[CreateAssetMenu(menuName = "States/SpecialAttack")]
public class RubiBossSpecialAttackState : EnemyState
{
    [SerializeField] float attackDelay = 0.5f;
    [SerializeField] float attackDuration = 1f;
    public override void Enter(Enemy enemy)
    {
        enemy.hasAttacked = false;
        enemy.StopMoving();
        enemy.stateTimer = Time.time;
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        enemy.stateTimer += deltaTime;

        if(enemy.stateTimer >= attackDelay && !enemy.hasAttacked)
        {
            enemy.DamageTarget(enemy.flyweightData.damage);
            enemy.hasAttacked = false;
        }

        if (enemy.stateTimer >= attackDuration)
        {
            if (enemy.DistanceWithPlayer() < enemy.flyweightData.reachPlayerRadius)
            {
                enemy.ChangeState(enemy.flyweightData.chaseState);
            }
            else
            {
                enemy.ChangeState(enemy.flyweightData.idleState);
            }
        }
    }
    public override void Exit(Enemy enemy)
    {

    }
}
