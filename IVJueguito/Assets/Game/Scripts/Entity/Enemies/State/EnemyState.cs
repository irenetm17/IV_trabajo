using UnityEngine;

public abstract class EnemyState : ScriptableObject
{
    public abstract void Enter(Enemy enemy);
    public abstract void Execute(Enemy enemy, float deltaTime);
    public abstract void Exit(Enemy enemy);
}
