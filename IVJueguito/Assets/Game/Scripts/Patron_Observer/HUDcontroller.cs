using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class HUDcontroller : MonoBehaviour, IObserver
{
    Image RubiIcon;
    TextMeshPro vidas;
    [SerializeField] private TextMeshProUGUI healthUI;
    [SerializeField] private TextMeshProUGUI gemasUI;
    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.PlayerStatsUpdated)
        {
            PlayerStatsEvent event2 = (PlayerStatsEvent)evento; //desempaqueta
            if (event2.health==3)
            {
                healthUI.text = "Health: 3";
            }
            else if (event2.health == 2)
            {
                healthUI.text = "Health: 2";
            }
            else if (event2.health == 1)
            {
                healthUI.text = "Health: 1";
            }
            else if (event2.health == 0)
            {
                healthUI.text = "Health: 0";
            }

            switch (event2.gems)
            {
                case 1:
                    gemasUI.text = "Gemas: 1";
                    break;
                case 2:
                    gemasUI.text = "Gemas: 2";
                    break;
                case 3:
                    gemasUI.text = "Gemas: 3";
                    break;
                case 4:
                    gemasUI.text = "Gemas: 4";
                    break;
            }
        }
    }
    void Start()
    {
        EventManager.instance.Subscribir(eventType.PlayerStatsUpdated, this);
    }
    void OnDestroy()
    {
        if (EventManager.instance!=null)
        {
            EventManager.instance.Desuscribir(eventType.PlayerStatsUpdated, this);
        }
    }
}
