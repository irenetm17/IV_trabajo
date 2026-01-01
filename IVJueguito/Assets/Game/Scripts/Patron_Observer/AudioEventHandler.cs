using UnityEngine;

public class AudioEventHandler : IObserver
{
    void Start()
    {
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);
        EventManager.instance.Subscribir(eventType.PlayerStatsUpdated, this);
    }
    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.CollectiblePicked)
        {
            CollectibleEvent event2 = (CollectibleEvent)evento; //desempaqueta
            if(event2.tipo == CollectibleType.Gema)
            {
                AudioService.instance.PlaySFX("GemaPickup");
            }
            else if(event2.tipo ==CollectibleType.Llaves)
            {
                AudioService.instance.PlaySFX("KeyPickup");
            }
        }
    }
}
