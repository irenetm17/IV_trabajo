using UnityEngine;

public class Sapphire : MonoBehaviour
{
    private Vector3 startPosition;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.localPosition;
    }

    public void Init()
    {
        transform.localPosition = startPosition;
    }

    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
    }


    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player == null)
        {
            if (enemy != null)
            {
                gameObject.SetActive(false);
                // Bajar vida a enemigos

                DamageTakenEvent pegarEnemigos = new DamageTakenEvent(enemy, 1.0f);
                EventManager.instance.Publicar(pegarEnemigos);
            }
        }
    }
}
