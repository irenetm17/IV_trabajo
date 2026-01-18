using UnityEngine;

public enum OpenableState
{
    Closed,     // cerrada, no usable
    Unlockable, // cerrada pero se puede abrir (tienes llave)
    Open        // abierta
}

public abstract class Openable : MonoBehaviour
{
    [SerializeField] protected OpenableState state;

    public OpenableState State => state;

    public bool IsOpen => state == OpenableState.Open;

    public abstract void SetState(OpenableState newState);
}
