using UnityEngine;

public enum CollectibleType
{
    Corazones,
    Llaves,
    Gema
}

public class CollectibleEvent : IEvent //evento que indica que se ha recogido un objeto y la cantidad de puntos que da
{
    public eventType Tipo { 
        get { 
            return eventType.CollectiblePicked; 
        } 
    }
    public int amount;

    public CollectibleType tipo;

    public CollectibleEvent(CollectibleType tipo, int amount)
    {
        this.tipo = tipo;
        this.amount = amount;
    }
}
