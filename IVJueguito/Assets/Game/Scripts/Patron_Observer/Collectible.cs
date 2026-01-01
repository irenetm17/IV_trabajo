using UnityEngine;

public class Collectible : MonoBehaviour //se asigna a los objetos para que puedan llamar a collectible event con x propiedades
{
    [SerializeField] private CollectibleType tipo;
    [SerializeField] private int amount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectibleEvent collectibleEvent = new CollectibleEvent(tipo, amount);
            EventManager.instance.Publicar(collectibleEvent);
            Destroy(gameObject);
        }
    }
}
