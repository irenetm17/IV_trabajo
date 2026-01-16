using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlayerMovement : Entity, IObserver
{
    
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector2 _moveDirection;

    private bool canMove = true;
    public bool CanMove => canMove;
    public InputActionReference move;
    private Animator animator;
    private Transform hijo;

    void Start()
    {
        EventManager.instance.Subscribir(eventType.PlayerCanMove, this);
        animator = GetComponentInChildren<Animator>();
        hijo = transform.GetChild(0);
    }
    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.PlayerCanMove)
        {
            canMove = !canMove;
        }
    }
    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.PlayerCanMove, this);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update()
    {
        if (!canMove)
        {
            animator.SetFloat("speed", 0f);
            return;
        }
        // Detectar tecla mantenida en lugar de solo la pulsación del frame
        if (Keyboard.current != null)
        {
            bool front = Keyboard.current.sKey.isPressed;
            bool back = Keyboard.current.wKey.isPressed;
            bool right =  Keyboard.current.dKey.isPressed;
            bool left = Keyboard.current.aKey.isPressed;
            if (right)
            {
                hijo.localScale = new Vector3(Mathf.Abs(hijo.localScale.x) * -1, hijo.localScale.y, hijo.localScale.z);
            }
            if(left)
            {
                hijo.localScale = new Vector3(Mathf.Abs(hijo.localScale.x), hijo.localScale.y, hijo.localScale.z);
            }
            if (animator != null)
            {
                animator.SetBool("front", front);
                animator.SetBool("back", back);
                animator.SetBool("lateral", left||right);
            }
        }


        _moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * _moveDirection.y + right * _moveDirection.x;

        _rb.linearVelocity = new Vector3(moveDir.x * _moveSpeed, Physics.gravity.y, moveDir.z * _moveSpeed);
        if (_moveDirection != Vector2.zero)
        {
            AudioService.instance.PlaySFX("CaminarMago");
            animator.SetFloat("speed", 1.0f);
        }
        else
        {
            animator.SetFloat("speed", 0f);
        }
    }


}
