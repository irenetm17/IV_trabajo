using UnityEngine;

public interface IObjectPool
{
    void StartPool();

    IPoolObject TakeFromPool();

    void PutToPool(IObjectPool obj);

}
