using UnityEngine;

public interface IEnemy
{
    public void Initialize(EnemyType type);
    public void ChangeState(EnemyState newState);
    public bool IsAlive();
    public void Spawn(Vector3 pos);
    public void TakeDamage(float damage);
    public void DamageTarget(float damageDealt);
    public void MoveTo(Vector3 target);
    public void StopMoving();
    public Vector3 SearchPlayer();
    public float DistanceWithPlayer();
    public Vector3 GetRandomWayPoint(); // Devuelve un punto aleatorio para patrullar
}
