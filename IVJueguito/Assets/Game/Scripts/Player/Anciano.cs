using UnityEngine;

public class Anciano : MonoBehaviour, IObserver
{
    [SerializeField] public string[] textoDiamante;
    [SerializeField] public string[] textoRubi;
    [SerializeField] public string[] textoZafiro;
    [SerializeField] public string[] textoEsmeralda;
    private int numGemas = 0;
    private HablarInteractuar hablar;

    void Start()
    {
        EventManager.instance.Subscribir(eventType.PlayerStatsUpdated, this);
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);

        hablar = GetComponent<HablarInteractuar>();
    }
    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.PlayerStatsUpdated)
        {
            PlayerStatsEvent event2 = (PlayerStatsEvent)evento;
            if (event2.gems != 0)
            {
                numGemas += event2.gems;
                numGemas = Mathf.Clamp(numGemas, 0, 4);

                SetTextoSegunGemas();
            }
        }
        if (evento.Tipo == eventType.CollectiblePicked)
        {
            CollectibleEvent event4 = (CollectibleEvent)evento; //desempaqueta
            if (event4.tipo == CollectibleType.Gema)
            {
                numGemas += event4.amount;
                numGemas = Mathf.Clamp(numGemas, 0, 4);
                SetTextoSegunGemas();
            }
        }

    }

    private void SetTextoSegunGemas()
    {
        switch (numGemas)
        {
            case 0:
            case 1:
                hablar.arrayTextos = textoDiamante;
                break;
            case 2:
                hablar.arrayTextos = textoRubi;
                break;
            case 3:
                hablar.arrayTextos = textoZafiro;
                break;
            case 4:
                hablar.arrayTextos = textoEsmeralda;
                break;
            default:
                hablar.arrayTextos = new string[] { "Error en el conteo de gemas." };
                break;
        }
    }

    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.PlayerStatsUpdated, this);
            EventManager.instance.Desuscribir(eventType.CollectiblePicked, this);
        }
    }

}
