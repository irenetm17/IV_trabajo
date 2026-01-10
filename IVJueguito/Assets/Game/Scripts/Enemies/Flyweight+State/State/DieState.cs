using UnityEngine;

[CreateAssetMenu(menuName = "States/Die")]
public class DieState : EnemyState
{
    private float dieTime = 1f;
    public override void Enter(Enemy enemy)
    {
        enemy.stateTimer = 0;
        enemy.animator.SetBool("alive", false);
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        enemy.stateTimer += deltaTime;
        if(enemy.stateTimer > dieTime)
        {
            Destroy(enemy);
        }
        
    }
    public override void Exit(Enemy enemy)
    {
        enemy.animator.SetBool("alive", false);
    }
}
