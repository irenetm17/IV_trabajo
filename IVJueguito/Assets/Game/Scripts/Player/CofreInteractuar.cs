using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class CofreInteractuar : Openable
{
    [SerializeField] private float distanceToInteract = 3f;
    public InputActionReference input;

    [SerializeField] private GameObject F;

    [Header("COFRE")]
    [SerializeField] private CollectibleType cosaDelCofre;
    [SerializeField] private GameObject imagenDelCofre;

    private PlayerMovement player;
    private float distance;
    private bool interactuado = false;

    private Animator animator;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if ((distance <= distanceToInteract) && (player.CanMove) && (!interactuado))
        {
            F.SetActive(true);
            if (input.action.WasPerformedThisFrame())
            {
                Cofre();
            }
        }
        else
        {
            F.SetActive(false);
        }
    }

    void Cofre()
    {
        interactuado = true;//Esto marca que se ha abierto el cofre y no se va a abrir mas
        SimpleEvent quieto = new SimpleEvent(eventType.PlayerCanMove);
        EventManager.instance.Publicar(quieto);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Cesar aqui va lo del cofre abriendose, solo animacion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        animator.SetBool("Abrir", true);

        StartCoroutine(MoveUp(10f));
    }
    public IEnumerator MoveUp(float speed)
    {
        yield return new WaitForSeconds(0.5f);
        imagenDelCofre.gameObject.SetActive(true);
        CollectibleEvent collectibleEvent = new CollectibleEvent(cosaDelCofre, 1);
        EventManager.instance.Publicar(collectibleEvent);

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

    public override void SetState(bool open)
    {
        isOpen = open;

        if (open)
        {
            interactuado = true;
            animator.SetBool("Abrir", true);
        }
    }

}
