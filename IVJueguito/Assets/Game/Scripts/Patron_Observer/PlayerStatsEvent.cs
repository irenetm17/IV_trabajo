using UnityEngine;

public class PlayerStatsEvent : IEvent
{
    public eventType Tipo
    {
        get
        {
            return eventType.PlayerStatsUpdated;
        }
    }

    public float health;
    public int gems;

    public PlayerStatsEvent(float health, int gems)
    {
        this.health = health;
        this.gems = gems;
    }
}
