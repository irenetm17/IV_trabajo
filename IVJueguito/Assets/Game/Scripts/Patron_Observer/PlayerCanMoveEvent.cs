using UnityEngine;

public class PlayerCanMoveEvent : IEvent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public eventType Tipo
    {
        get
        {
            return eventType.PlayerCanMove;
        }
    }
    public bool canMove;


    public PlayerCanMoveEvent(bool c)
    {
        this.canMove = c;
    }
}
