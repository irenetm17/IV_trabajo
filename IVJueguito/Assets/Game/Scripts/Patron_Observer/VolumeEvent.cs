using UnityEngine;

public class VolumeEvent : IEvent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public eventType Tipo
    {
        get
        {
            return eventType.VolumeChanged;
        }
    }
    public float volumen;


    public VolumeEvent(float volumen)
    {
        this.volumen = volumen;
    }
}
