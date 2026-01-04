using UnityEngine;

public class DamageTakenEvent : IEvent
{
    public eventType Tipo
    {
        get
        {
            return eventType.PlayerStatsUpdated;
        }
    }

    public GameObject Target { get; private set; } 
    public int Amount { get; private set; }

    // Constructor
    public DamageTakenEvent(GameObject target)
    {
        Target = target;
    }
}
