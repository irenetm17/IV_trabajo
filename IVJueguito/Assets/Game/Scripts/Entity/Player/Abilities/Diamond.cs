using UnityEngine;

public class Diamond : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        // Golpear enemigo
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Bajar vida a enemigos
            Debug.Log("Le hice pupa");
        }

        MuroRocaDiamante muroRocaDiamante = other.GetComponent<MuroRocaDiamante>();
        if (muroRocaDiamante != null)
        {
            muroRocaDiamante.DestrozarMuro();
        }

    }

}
