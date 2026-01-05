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

    void Start()
    {
        EventManager.instance.Subscribir(eventType.PlayerCanMove, this);
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
        if (!canMove) return;
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
    }


}
