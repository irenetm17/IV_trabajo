using UnityEngine;

public class Emerald : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Proyectile p = other.GetComponent<Proyectile>();
        if (p != null)
        {
            p.Reverse();
            Debug.LogWarning("Reverse");
        }
    }
}
