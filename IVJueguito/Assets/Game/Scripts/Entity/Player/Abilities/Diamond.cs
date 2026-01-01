using UnityEngine;

public class Diamond : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Bajar vida a enemigos
            Debug.LogWarning("Le hice pupa");
        }
    }
}
