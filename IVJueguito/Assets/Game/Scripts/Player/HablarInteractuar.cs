using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class HablarInteractuar : MonoBehaviour
{
    [SerializeField] private float distanceToInteract = 3f;
    public InputActionReference input;

    [SerializeField] private GameObject F;
    [Header("HABLAR")]
    [SerializeField] public string[] arrayTextos;
    public bool puedeHablar = true;

    private PlayerMovement player;
    private float distance;
    private bool interactuado = false;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (puedeHablar==true)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
            if ((distance <= distanceToInteract) && (player.CanMove) && (!interactuado))
            {
                F.SetActive(true);
                if (input.action.WasPerformedThisFrame())
                {
                    Hablar();
                }

            }
            else
            {
                F.SetActive(false);
            }
        }
    }

    public void Hablar()
    {
        DialogueStartedEvent dialogoEvento = new DialogueStartedEvent(arrayTextos);
        EventManager.instance.Publicar(dialogoEvento);
    }
}
