using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class LlaveInteractuar : Openable, IObserver
{
    [SerializeField] private float distanceToInteract = 3f;
    public InputActionReference input;

    [SerializeField] private GameObject F;

    [Header("PUERTA")]
    [SerializeField] private string[] arrayTextos;

    private PlayerMovement player;
    private float distance;
    private bool interactuado = false;
    private int numLlaves = 0;

    private Animator animator;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        EventManager.instance.Subscribir(eventType.UseKey, this);
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);
    }

    public void OnEvent(IEvent evento)
    {
        

    }
    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.UseKey, this);
            EventManager.instance.Desuscribir(eventType.CollectiblePicked, this);
        }
    }
    public override void SetState(OpenableState newState)
    {
        
    }


}
