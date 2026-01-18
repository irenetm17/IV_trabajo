using UnityEngine;

[CreateAssetMenu(menuName = "States/ZafireSpecial")]
public class ZafireBossSpecialGimmick : EnemyState
{
    [SerializeField] float delayTime = 0.5f;
    [SerializeField] float impulseForce = 100f;
    public override void Enter(Enemy enemy)
    {
        MiniBoss boss = enemy as MiniBoss;
        if (boss == null) return;
        Debug.Log($"<color=cyan>{enemy.name}</color> ha entrado en el estado: <color=yellow>{this.name}</color>");

        enemy.StopMoving();
        enemy.stateTimer = 0;
        boss.pushBack();
        boss.Impulse(impulseForce);
    }
    public override void Execute(Enemy enemy, float deltaTime)
    {
        enemy.stateTimer += deltaTime;
        if (enemy.stateTimer > delayTime)
        {
            enemy.ChangeState(enemy.flyweightData.idleState);
        }
    }
    public override void Exit(Enemy enemy)
    {
        MiniBoss boss = enemy as MiniBoss;
        if (boss == null) return;
        boss.SpawnSlimes();
    }
}
