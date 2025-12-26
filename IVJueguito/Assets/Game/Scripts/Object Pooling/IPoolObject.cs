using UnityEngine;

public interface IPoolObject
{
    bool isShowed();
    void  SetActive(bool active);
    void  ResetObject();
}
