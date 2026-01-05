using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class LlaveInteractuar : MonoBehaviour
{
    [SerializeField] private float distanceToInteract = 3f;
    public InputActionReference input;

    [SerializeField] private GameObject F;

    [Header("PUERTA")]

    private PlayerMovement player;
    private float distance;
    private bool interactuado = false;
    private int numLlaves = 0;


    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
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
        interactuado = true;//Esto marca que se ha abierto la puerta y no se va a abrir mas
        //Esto es para parar al jugador mientras se abre la puerta
        SimpleEvent quieto = new SimpleEvent(eventType.PlayerCanMove);
        EventManager.instance.Publicar(quieto);

        //A esta mierda le quedan cosas por hacer

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Cesar aqui va lo de la puerta abriendose
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Despues de abrir la puerta, usaremos esto para volver a dejar al jugador moverse
        SimpleEvent muevete = new SimpleEvent(eventType.PlayerCanMove);
        EventManager.instance.Publicar(muevete);
    }
}
