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

    public int health;
    public int gems;

    public PlayerStatsEvent(int health, int gems)
    {
        this.health = health;
        this.gems = gems;
    }
}
