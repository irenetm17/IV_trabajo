using UnityEngine;

public class DoorOpenedEvent : IEvent
{
    public eventType Tipo
    {
        get
        {
            return eventType.DoorOpened;
        }
    }

    public PuertaAutomatica Target;
    public bool Abrir;

    // Constructor
    public DoorOpenedEvent(PuertaAutomatica target, bool abrir)
    {
        Target = target;
        Abrir = abrir;
    }
}
