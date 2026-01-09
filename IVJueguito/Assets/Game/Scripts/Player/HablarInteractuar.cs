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
    [SerializeField] private string[] arrayTextos;

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
                Hablar();
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
}
