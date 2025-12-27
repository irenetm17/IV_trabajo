using UnityEngine;

public interface IEnemy
{
    public void Initialize(EnemyType type);
    public void ChangeState(EnemyState newState);
    public bool IsAlive();
    public void Spawn(Vector3 pos);
    public void TakeDamage(int damage);
    public void MoveTo(Vector3 target);
    public void StopMoving();
}
