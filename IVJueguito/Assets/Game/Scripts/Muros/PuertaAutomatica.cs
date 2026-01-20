using UnityEngine;

public class PuertaAutomatica : Openable, IObserver
{

    private Animator animator;
    [SerializeField] private GameObject Light;
    [SerializeField] private Material GreenLight;
    [SerializeField] private Material RedLight;
    private HablarInteractuar hablar;

    void Start()
    {
        EventManager.instance.Subscribir(eventType.DoorOpened, this);
        animator = GetComponent<Animator>();
        hablar = GetComponent<HablarInteractuar>();
    }

    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.DoorOpened)
        {
            DoorOpenedEvent event4 = (DoorOpenedEvent)evento; //desempaqueta
            if (event4.Target != this) return;
            if (event4.Abrir)
            {
                AbrirPuerta();
            }
            else
            {
                CerrarPuerta();
            }
        }

    }
    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.DoorOpened, this);
        }
    }
    public override void SetState(OpenableState newState)
    {
        state = newState;

        if (state == OpenableState.Open)
            AbrirPuerta();
        else
            CerrarPuerta();
    }

    public void AbrirPuerta()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("AbrirPuerta", true);
        Light.GetComponent<Renderer>().material = GreenLight;
        hablar = GetComponent<HablarInteractuar>();
        if (hablar != null)
        {
            hablar.enabled = false;
        }
    }

    public void CerrarPuerta()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("AbrirPuerta", false);
        Light.GetComponent<Renderer>().material = RedLight;
        hablar = GetComponent<HablarInteractuar>();
        if (hablar != null)
        {
            hablar.enabled = true;
        }
    }
}
