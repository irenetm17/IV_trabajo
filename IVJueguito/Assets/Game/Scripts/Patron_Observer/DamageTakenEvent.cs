using UnityEngine;

public class DamageTakenEvent : IEvent
{
    public eventType Tipo
    {
        get
        {
            return eventType.DamageTaken;
        }
    }

    public Enemy Target;
    public float Amount;

    // Constructor
    public DamageTakenEvent(Enemy target, float damage)
    {
        Target = target;
        Amount = damage;
    }
}
