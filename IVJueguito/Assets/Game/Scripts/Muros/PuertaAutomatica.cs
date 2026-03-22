using UnityEngine;
using UnityEngine.InputSystem;

public class PuertaAutomatica : Openable, IObserver
{

    [Header("Configuración de Tipo")]
    [SerializeField] private bool necesitaLlave = false;

    [Header("Interacción")]
    [SerializeField] private float distanceToInteract = 3f; // Tu variable original
    public InputActionReference input; // Tu referencia original
    [SerializeField] private GameObject F; // Tu objeto F original

    [Header("Visuales")]
    [SerializeField] private GameObject luzObjeto;
    [SerializeField] private Material matVerde;
    [SerializeField] private Material matRojo;
    [SerializeField] private Material matNaranja;

    private PlayerMovement player;
    private Animator animator;
    private HablarInteractuar hablar;
    private float distance; // Tu variable de cálculo original
    private bool yaSeAbrioConLlave = false;
    private int numLlaves = 0; // Tu variable original

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        hablar = GetComponent<HablarInteractuar>();
    }

    void Start()
    {
        EventManager.instance.Subscribir(eventType.UseKey, this);
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);
        EventManager.instance.Subscribir(eventType.DoorOpened, this);

        // Al iniciar o cargar, forzamos el estado visual
        RefrescarPuerta();
    }

    void Update()
    {
        // --- TU LÓGICA DE DISTANCIA ORIGINAL ---
        if (player == null) return;

        distance = Vector3.Distance(player.transform.position, transform.position);

        // Condición: Si estoy cerca Y el jugador puede moverse
        if (distance <= distanceToInteract && player.CanMove)
        {
            // Solo mostramos la F si la puerta está cerrada (ya sea por llave o automática)
            if (state == OpenableState.Closed)
            {
                F.SetActive(true);

                if (input.action.WasPerformedThisFrame())
                {
                    if (necesitaLlave && !yaSeAbrioConLlave)
                    {
                        PuertaConLlave();
                    }
                    else
                    {
                        // Si es automática, al pulsar F el script HablarInteractuar 
                        // saltará solo porque el componente está activo.
                        Debug.Log("Interactuando con puerta automática cerrada");
                    }
                }
            }
            else
            {
                F.SetActive(false);
            }
        }
        else
        {
            F.SetActive(false);
        }
    }

    void PuertaConLlave()
    {
        if (numLlaves > 0)
        {
            yaSeAbrioConLlave = true;
            state = OpenableState.Open;

            // Publicamos eventos para que el mundo se entere
            EventManager.instance.Publicar(new SimpleEvent(eventType.UseKey));

            RefrescarPuerta();
            Debug.LogWarning("Puerta abierta con llave");
        }
        else
        {
            // Aquí no hacemos nada, el script HablarInteractuar ya dirá que falta la llave
            Debug.LogWarning("No tienes llave");
        }
    }

    // --- SISTEMA DE EVENTOS Y GUARDADO ---
    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.CollectiblePicked)
        {
            CollectibleEvent e = (CollectibleEvent)evento;
            if (e.tipo == CollectibleType.Llaves) numLlaves += e.amount;
        }

        if (evento.Tipo == eventType.UseKey) numLlaves--;

        if (evento.Tipo == eventType.DoorOpened)
        {
            DoorOpenedEvent e = (DoorOpenedEvent)evento;
            if (e.Target.gameObject == this.gameObject)
            {
                state = e.Abrir ? OpenableState.Open : OpenableState.Closed;
                if (necesitaLlave) yaSeAbrioConLlave = true;
                RefrescarPuerta();
            }
        }
    }

    public override void SetState(OpenableState newState)
    {
        state = newState;
        if (state == OpenableState.Open) yaSeAbrioConLlave = true;
        RefrescarPuerta();
    }

    private void RefrescarPuerta()
    {
        if (animator == null) animator = GetComponent<Animator>();

        if (state == OpenableState.Open)
        {
            animator.SetBool("AbrirPuerta", true);
            SetLuz(matVerde);
            if (hablar != null) hablar.enabled = false;
        }
        else
        {
            animator.SetBool("AbrirPuerta", false);
            if (necesitaLlave && !yaSeAbrioConLlave)
            {
                SetLuz(matNaranja);
                if (hablar != null) hablar.enabled = true;
            }
            else
            {
                SetLuz(matRojo);
                if (hablar != null) hablar.enabled = true;
            }
        }
    }

    private void SetLuz(Material m)
    {
        if (luzObjeto != null) luzObjeto.GetComponent<Renderer>().material = m;
    }

    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.UseKey, this);
            EventManager.instance.Desuscribir(eventType.CollectiblePicked, this);
            EventManager.instance.Desuscribir(eventType.DoorOpened, this);
        }
    }
}