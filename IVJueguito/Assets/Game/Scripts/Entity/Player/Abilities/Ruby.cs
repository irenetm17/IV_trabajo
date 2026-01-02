using UnityEngine;

public class Ruby : MonoBehaviour
{
    [SerializeField] private float lifeTime = 10f;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float maxDistance = 500f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector3 startPosition;


    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;

        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void Init(Vector3 direction)
    {
        direction.y = 0f; // no apuntar hacia arriba y abajo
        moveDirection = direction.normalized;

        transform.forward = moveDirection;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(
            moveDirection.x * speed,
            rb.linearVelocity.y,    // dejar que la gravedad haga su trabajo
            moveDirection.z * speed
        );

        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player == null)
        {
            if (enemy != null)
            {
                // Bajar vida a enemigos
                Debug.LogWarning("Le hice pupa");
                Destroy(gameObject);
            }
        }
    }
}
