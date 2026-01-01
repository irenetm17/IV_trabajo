using UnityEngine;

public interface IObjectPool
{
    void StartPool(); //Inicializar la pool

    IPoolObject TakeFromPool(); //Pillar un objeto de la pool y resetearlo para activarlo

    void PutToPool(IPoolObject obj);

}
