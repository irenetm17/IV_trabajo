using UnityEngine;

[CreateAssetMenu(menuName = "States/SpecialAttack")]
public class RubiBossSpecialAttackState : EnemyState
{
    public float chargeTime = 5f;
    public float windRadius = 30f;
    public float windForce = 20f;
    public float startTime;
    public override void Enter(Enemy enemy)
    {
        enemy.StopMoving();
        startTime = Time.time;
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        float timeElapsed = Time.time - startTime;
        float dist = enemy.DistanceWithPlayer();

        if(timeElapsed > chargeTime)
        {
            enemy.ChangeState();
        }

        if (dist < windRadius)
        {
            enemy.pushPlayer(windForce);
        }

        if (enemy.TakeDamage())
        {
            enemy.ChangeState(stuntBossState);
        }
    }
    public override void Exit(Enemy enemy)
    {

    }
}
