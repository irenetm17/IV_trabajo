using UnityEngine;

public class MuroOjosEsmeralda : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _partes;

    [SerializeField]
    private Collider _colliderMuro;

    private void Start()
    {
        _colliderMuro = GetComponent<Collider>();
    }


    public void DestrozarMuro()
    {
        // Desactivar collider
        _colliderMuro.enabled = false;

        for (int i = 0;  i < _partes.Length; i++)
        {
            //Añadir Rigidbodys
            _partes[i].AddComponent<Rigidbody>();

            //Que desaparezcan a los pocos segundos
            Destroy( _partes[i], Random.Range(4,7));

        }
    }


}
