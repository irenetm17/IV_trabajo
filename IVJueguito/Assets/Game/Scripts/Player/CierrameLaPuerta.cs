using UnityEngine;

public class CierrameLaPuerta : MonoBehaviour
{
    private AbrirPuertas abrirPuertas;

    void Start()
    {
        abrirPuertas = GetComponent<AbrirPuertas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (abrirPuertas != null)
            {
                abrirPuertas.AbrirCerrarPuertas();
            }
            Destroy(gameObject);
        }
    }

}
