using UnityEngine;

public class AbrirPuertas : MonoBehaviour
{
    [SerializeField] private PuertaAutomatica[] puertasLista;
    [SerializeField] private bool[] puertasAbrir;

    public void AbrirCerrarPuertas()
    {
        for (int i = 0; i < puertasLista.Length; i++)
        {
            DoorOpenedEvent doorEvent = new DoorOpenedEvent(puertasLista[i], puertasAbrir[i]);
            EventManager.instance.Publicar(doorEvent);
        }
    }

}
