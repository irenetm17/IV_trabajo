using UnityEngine;

[CreateAssetMenu(menuName = "States/Die")]
public class DieState : EnemyState
{
    private float dieTime = 1f;
    public override void Enter(Enemy enemy)
    {
        enemy.stateTimer = 0;
        enemy.animator.SetBool("alive", false);

        enemy.KillEnemy();
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        enemy.KillEnemy();

        /*enemy.stateTimer += deltaTime;
        if(enemy.stateTimer > dieTime)
        {
            enemy.KillEnemy();
        }*/        
    }
    public override void Exit(Enemy enemy)
    {
        enemy.animator.SetBool("alive", false);
    }
}
