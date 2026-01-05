using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class LlaveInteractuar : MonoBehaviour, IObserver
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


    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        EventManager.instance.Subscribir(eventType.UseKey, this);
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);
    }

    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.UseKey)
        {
            numLlaves--;
        }
        if (evento.Tipo == eventType.CollectiblePicked)
        {
            CollectibleEvent event4 = (CollectibleEvent)evento; //desempaqueta
            if (event4.tipo == CollectibleType.Llaves)
            {
                numLlaves += event4.amount;
            }
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

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if ((distance <= distanceToInteract) && (player.CanMove) && (!interactuado))
        {
            F.SetActive(true);
            if (input.action.WasPerformedThisFrame())
            {
                Puerta();
            }

        }
        else
        {
            F.SetActive(false);
        }
    }

    void Puerta()
    {
        if(numLlaves > 0)
        {
            interactuado = true;//Esto marca que se ha abierto la puerta y no se va a abrir mas
            //Esto es para parar al jugador mientras se abre la puerta
            SimpleEvent quieto = new SimpleEvent(eventType.PlayerCanMove);
            EventManager.instance.Publicar(quieto);
            EventManager.instance.Publicar(new SimpleEvent(eventType.UseKey)); //Publica el evento de usar llave

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Cesar aqui va lo de la puerta abriendose
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Despues de abrir la puerta, usaremos esto para volver a dejar al jugador moverse
            SimpleEvent muevete = new SimpleEvent(eventType.PlayerCanMove);
            EventManager.instance.Publicar(muevete);
            Debug.LogWarning("Puerta abierta");
        }
        else
        {
            if(arrayTextos != null && arrayTextos.Length > 0)
            {
                DialogueStartedEvent dialogoEvento = new DialogueStartedEvent(arrayTextos);
                EventManager.instance.Publicar(dialogoEvento);
            }
            Debug.LogWarning("No tienes llave");
            return;
        }

    }
}
