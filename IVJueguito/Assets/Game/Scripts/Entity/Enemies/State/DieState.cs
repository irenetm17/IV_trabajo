using UnityEngine;

[CreateAssetMenu(menuName = "States/Die")]
public class DieState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        // Animación de muerte, desactivar collider
        enemy.animator.SetBool("alive", false);
    }
    public override void Execute(Enemy enemy, float deltaTime){}
    public override void Exit(Enemy enemy)
    {
        enemy.animator.SetBool("alive", false);
    }
}
