using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.AchievementUnlocked)
        {
            SimpleEvent event2 = (SimpleEvent)evento; //desempaqueta
            //aqui meter logica de logros uwu
        }
    }
    void Start()
    {
        EventManager.instance.Subscribir(eventType.AchievementUnlocked, this);
    }
    private void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.AchievementUnlocked, this);
        }
    }
}
