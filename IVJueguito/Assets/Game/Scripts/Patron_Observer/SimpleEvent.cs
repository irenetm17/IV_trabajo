using UnityEngine;

public class SimpleEvent : IEvent //clase genérica para eventos que no necesitan datos adicionales
{
    private eventType tipo;
    public eventType Tipo
    {
        get
        {
            return tipo;
        }
    }
    public SimpleEvent(eventType tipo)
    {
        this.tipo = tipo;
    }
}
