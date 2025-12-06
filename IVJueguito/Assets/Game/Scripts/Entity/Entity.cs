using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Camera cam;
    protected Transform spritePivot;

    protected virtual void Awake()
    {
        cam = Camera.main;
        spritePivot = transform.GetChild(0); //El sprite debe ser el primer hijo
    }

    protected virtual void LateUpdate()
    {
        // Hacer que el sprite mire a la cámara
        spritePivot.forward = cam.transform.forward;
    }
}

