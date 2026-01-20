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
            DamageTakenEvent pegarEnemigos = new DamageTakenEvent(enemy, 2.0f);
            EventManager.instance.Publicar(pegarEnemigos);
            //Debug.Log("Le hice pupa");
        }

        MuroRocaDiamante muroRocaDiamante = other.GetComponent<MuroRocaDiamante>();
        if (muroRocaDiamante != null)
        {
            muroRocaDiamante.DestrozarMuro();
        }

    }

}
