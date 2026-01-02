using UnityEngine;

public class Proyectile : MonoBehaviour
{
    private float speed;
    private Vector3 direccion;

    void Start()
    {
        //ESTO ES PARA PROBAR COSAS
        Init(Vector3.left, 20f);
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

    void FixedUpdate()
    {
        transform.position += direccion * speed * Time.fixedDeltaTime;

    }
}
