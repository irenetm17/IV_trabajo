using UnityEngine;

public class PuertaAutomatica : Openable, IObserver
{
    private Animator animator;
    [SerializeField] private GameObject Light;
    [SerializeField] private Material GreenLight;
    [SerializeField] private Material RedLight;
    private HablarInteractuar hablar;
    private LlaveInteractuar scriptLlave;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hablar = GetComponent<HablarInteractuar>();
        scriptLlave = GetComponent<LlaveInteractuar>();
        EventManager.instance.Subscribir(eventType.DoorOpened, this);
    }

    void Start()
    {
        // Forzar estado visual correcto al cargar
        if (state == OpenableState.Open) AbrirPuerta(); else CerrarPuerta();
    }

    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.DoorOpened)
        {
            DoorOpenedEvent e = (DoorOpenedEvent)evento;
            if (e.Target != this) return;

            state = e.Abrir ? OpenableState.Open : OpenableState.Closed;
            if (scriptLlave != null) scriptLlave.SincronizarEstadoExterno(state);

            if (e.Abrir) AbrirPuerta(); else CerrarPuerta();
        }
    }

    public override void SetState(OpenableState newState)
    {
        if (scriptLlave != null) return; // Si hay llave, ella manda
        state = newState;
        if (state == OpenableState.Open) AbrirPuerta(); else CerrarPuerta();
    }

    public void AbrirPuerta()
    {
        if (animator == null) animator = GetComponent<Animator>();
        animator.SetBool("AbrirPuerta", true);
        if (Light != null) Light.GetComponent<Renderer>().material = GreenLight;
        if (hablar != null) hablar.enabled = false;
    }

    public void CerrarPuerta()
    {
        if (animator == null) animator = GetComponent<Animator>();
        animator.SetBool("AbrirPuerta", false);
        if (Light != null) Light.GetComponent<Renderer>().material = RedLight;
        if (hablar != null) hablar.enabled = true;
    }

    void OnDestroy()
    {
        if (EventManager.instance != null) EventManager.instance.Desuscribir(eventType.DoorOpened, this);
    }
}