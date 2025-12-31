using UnityEngine;

public interface IPoolObject
{
    bool isActive();
    void  SetActive(bool active);
    void  ResetObject();
    void MoveTo(Vector3 position);
}
