using UnityEngine;

[CreateAssetMenu(menuName = "States/Attack")]
public class AttackState : EnemyState
{
    [SerializeField] float attackDelay = 0.5f;
    [SerializeField] float attackDuration = 1f;

    public override void Enter(Enemy enemy)
    {
        Debug.Log($"<color=cyan>{enemy.name}</color> ha entrado en el estado: <color=yellow>{this.name}</color>");

        enemy.animator.SetBool("nearPlayer", false);
        enemy.StopMoving();
        enemy.stateTimer = 0f;
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        enemy.stateTimer += deltaTime;
        enemy.animator.SetBool("nearPlayer", true);

        if (enemy.stateTimer >= attackDelay)
        {
            
                if (enemy.DistanceWithPlayer() > enemy.flyweightData.reachPlayerRadius)
                {
                    enemy.ChangeState(enemy.flyweightData.chaseState);
                }
                else 
                {
                    enemy.DamageTarget(enemy.flyweightData.damage);
                    enemy.ChangeState(enemy.flyweightData.attackState);
                }
            
        }

    }
    public override void Exit(Enemy enemy)
    {
        //enemy.animator.SetBool("nearPlayer", true);
    }
}

