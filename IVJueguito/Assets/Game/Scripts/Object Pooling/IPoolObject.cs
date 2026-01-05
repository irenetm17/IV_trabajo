using UnityEngine;

public interface IPoolObject
{
    bool isActive();
    void  SetActive(bool active);
    void  ResetObject();
    void DisplaceTo(Vector3 position);
}
