using UnityEngine;

public class Proyectile : MonoBehaviour
{
    private float speed;
    private Vector3 direccion;

    [SerializeField]
    private bool _damagePlayer = false;

    void Start()
    {
        //ESTO ES PARA PROBAR COSAS
        //Init(Vector3.left, 20f);
    }

    public void Init(Vector3 dir, float spd)
    {
        direccion = dir;
        speed = spd;
    }

    public void Reverse()
    {
        direccion = -1 * direccion;
    }

    void Update()
    {
        transform.position += direccion * speed * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_damagePlayer)
            {
                // Hacer daño al jugador
                PlayerStatsEvent vidasRestar = new PlayerStatsEvent(-0.5f, 0);
                EventManager.instance.Publicar(vidasRestar);
            }
        }
    }

}
