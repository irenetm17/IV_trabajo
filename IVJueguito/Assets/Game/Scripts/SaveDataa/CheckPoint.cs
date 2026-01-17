using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SimpleEvent guardar = new SimpleEvent(eventType.GameSaved);
            EventManager.instance.Publicar(guardar);
            Destroy(gameObject);
        }
    }
}
