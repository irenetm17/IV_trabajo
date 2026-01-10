using UnityEngine;

[CreateAssetMenu(menuName = "States/SpecialAttack")]
public class RubiBossSpecialAttackState : EnemyState
{
    [SerializeField] float attackDelay = 0.5f;
    [SerializeField] float specialAttackdmg = 2f;

    public override void Enter(Enemy enemy)
    {
        MiniBoss boss = enemy as MiniBoss;
        Debug.Log($"<color=cyan>{enemy.name}</color> ha entrado en el estado: <color=yellow>{this.name}</color>");

        enemy.animator.SetBool("nearPlayer", false);
        enemy.StopMoving();
        enemy.stateTimer = 0f;
        boss.explosion.Play();
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        enemy.stateTimer += deltaTime;
        enemy.animator.SetBool("nearPlayer", true);
        enemy.DamageTarget(specialAttackdmg);

        if (enemy.stateTimer >= attackDelay)
        {

            if (enemy.DistanceWithPlayer() > enemy.flyweightData.reachPlayerRadius)
            {
                enemy.ChangeState(enemy.flyweightData.chaseState);
            }
            else
            {
                enemy.ChangeState(enemy.flyweightData.attackState);
            }

        }

    }
    public override void Exit(Enemy enemy)
    {
        //enemy.animator.SetBool("nearPlayer", true);
    }
}
