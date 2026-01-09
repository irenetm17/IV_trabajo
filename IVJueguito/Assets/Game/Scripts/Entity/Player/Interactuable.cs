using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public enum InteracTipo
{
    Hablar,
    Cofre,
    Puerta
}

public class Interactuable : MonoBehaviour
{
    [SerializeField] private InteracTipo tipoInteractuable;
    [SerializeField] private float distanceToInteract = 3f;
    public InputActionReference input;

    [SerializeField] private GameObject F;

    [Header("HABLAR")]
    [SerializeField] private string[] arrayTextos;

    [Header("COFRE")]
    [SerializeField] private CollectibleType cosaDelCofre;
    [SerializeField] private GameObject imagenDelCofre;

    [Header("PUERTA")]

    private PlayerMovement player;
    private float distance;
    private bool interactuado = false;


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
                switch(tipoInteractuable)
                {
                    case InteracTipo.Hablar:
                        Hablar();
                        break;
                    case InteracTipo.Cofre:
                        Cofre();
                        break;
                    case InteracTipo.Puerta:
                        Puerta();
                        break;
                }
            }
            
        }
        else
        {
            F.SetActive(false);
        }
    }

    void Hablar()
    {
        DialogueStartedEvent dialogoEvento = new DialogueStartedEvent(arrayTextos);
        EventManager.instance.Publicar(dialogoEvento);
    }
    void Cofre()
    {
        interactuado = true;//Esto marca que se ha abierto el cofre y no se va a abrir mas
        SimpleEvent quieto = new SimpleEvent(eventType.PlayerCanMove);
        EventManager.instance.Publicar(quieto);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Cesar aqui va lo del cofre abriendose, solo animacion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        imagenDelCofre.gameObject.SetActive(true);
        StartCoroutine(MoveUp(10f));
        CollectibleEvent collectibleEvent = new CollectibleEvent(cosaDelCofre, 1);
        EventManager.instance.Publicar(collectibleEvent);
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

    public IEnumerator MoveUp(float speed)
    {
        float parentY = imagenDelCofre.gameObject.transform.parent.position.y;
        while (imagenDelCofre.gameObject.transform.position.y < parentY)
        {
            imagenDelCofre.gameObject.transform.position += Vector3.up * speed * Time.deltaTime;

            if (imagenDelCofre.gameObject.transform.position.y >= parentY)
                break;

            yield return null;
        }
        yield return new WaitForSeconds(3);
        imagenDelCofre.gameObject.SetActive(false);

        SimpleEvent muevete = new SimpleEvent(eventType.PlayerCanMove);
        EventManager.instance.Publicar(muevete);
    }

}
