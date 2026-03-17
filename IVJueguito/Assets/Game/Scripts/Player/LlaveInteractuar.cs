using UnityEngine;
using UnityEngine.InputSystem;

public class LlaveInteractuar : Openable, IObserver
{
    [SerializeField] private float distanceToInteract = 3f;
    public InputActionReference input;
    [SerializeField] private GameObject F;
    [SerializeField] private string[] arrayTextos;

    private PlayerMovement player;
    private bool interactuado = false;
    private int numLlaves = 0;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    void Start()
    {
        EventManager.instance.Subscribir(eventType.UseKey, this);
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);

        // Sincronización inicial al cargar partida
        ActualizarVisualPart(state == OpenableState.Open);
    }

    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.UseKey) numLlaves--;
        if (evento.Tipo == eventType.CollectiblePicked)
        {
            CollectibleEvent e = (CollectibleEvent)evento;
            if (e.tipo == CollectibleType.Llaves) numLlaves += e.amount;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= distanceToInteract && player.CanMove && !interactuado)
        {
            F.SetActive(true);
            if (input.action.WasPerformedThisFrame()) Puerta();
        }
        else
        {
            F.SetActive(false);
        }
    }

    void Puerta()
    {
        if (numLlaves > 0)
        {
            SincronizarEstadoExterno(OpenableState.Open);
            EventManager.instance.Publicar(new SimpleEvent(eventType.UseKey));
            EventManager.instance.Publicar(new PlayerCanMoveEvent(true));
        }
        else if (arrayTextos != null && arrayTextos.Length > 0)
        {
            EventManager.instance.Publicar(new PlayerCanMoveEvent(false));
            EventManager.instance.Publicar(new DialogueStartedEvent(arrayTextos));
        }
    }

    public override void SetState(OpenableState newState)
    {
        state = newState;
        interactuado = (state == OpenableState.Open);
        ActualizarVisualPart(interactuado);
    }

    public void SincronizarEstadoExterno(OpenableState nuevoEstado)
    {
        state = nuevoEstado;
        interactuado = (state == OpenableState.Open);
        ActualizarVisualPart(interactuado);
    }

    private void ActualizarVisualPart(bool abrir)
    {
        PuertaAutomatica pa = GetComponent<PuertaAutomatica>();
        if (pa != null)
        {
            if (abrir) pa.AbrirPuerta(); else pa.CerrarPuerta();
        }
    }

    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.UseKey, this);
            EventManager.instance.Desuscribir(eventType.CollectiblePicked, this);
        }
    }

    public void SetLlavesInternas(int cantidad) => numLlaves = cantidad;
}