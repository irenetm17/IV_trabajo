using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Entity
{
    
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector2 _moveDirection;


    public InputActionReference move;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Dirección de la cámara
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        // Eliminamos la coordenada Y de la cámara
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Convertimos el input a movimiento relativo a la cámara
        Vector3 moveDir = forward * _moveDirection.y + right * _moveDirection.x;

        // Aplicamos velocidad
        _rb.linearVelocity = moveDir * _moveSpeed;
    }


}
