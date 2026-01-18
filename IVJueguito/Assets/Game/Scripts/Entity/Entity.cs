using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Camera cam;
    protected Transform spritePivot;



    protected virtual void Awake()
    {
        //El sprite debe ser el primer hijo
        spritePivot = transform.GetChild(0); 
    }

    protected virtual void OnEnable()
    {
        cam = Camera.main;
       
        if (spritePivot != null && cam != null)
        {
            spritePivot.forward = cam.transform.forward;
        }
    }
    protected virtual void LateUpdate()
    {
        if (cam == null) return;
        // Hacer que el sprite mire a la cámara
        spritePivot.forward = cam.transform.forward;
    }
}

