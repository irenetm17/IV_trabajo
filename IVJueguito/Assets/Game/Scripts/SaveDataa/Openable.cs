using UnityEngine;

public abstract class Openable : MonoBehaviour
{
    [SerializeField] protected bool isOpen;

    public bool IsOpen => isOpen;

    public abstract void SetState(bool open);
}
