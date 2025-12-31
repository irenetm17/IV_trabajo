using UnityEngine;

public class Slime : IPoolObject  // Tambien implementará el Enemy
{
    public SlimePool parentSlimePool;

    public bool isActive()
    {
        throw new System.NotImplementedException();
    }

    public void ResetObject()
    {
        throw new System.NotImplementedException();
    }

    public void SetActive(bool active)
    {
        throw new System.NotImplementedException();
    }

}
