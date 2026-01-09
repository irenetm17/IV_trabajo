using UnityEngine;

public class MuroRocaDiamante : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _rocas;

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

        for (int i = 0;  i < _rocas.Length; i++)
        {
            //Añadir Rigidbodys
            _rocas[i].AddComponent<Rigidbody>();

            //Que desaparezcan a los pocos segundos
            Destroy( _rocas[i], Random.Range(4,7));

        }
    }


}
