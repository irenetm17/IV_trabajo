using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioEventHandler : MonoBehaviour, IObserver
{
    [SerializeField] private AudioMixer audioMixer;
    void Start()
    {
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);
        EventManager.instance.Subscribir(eventType.PlayerStatsUpdated, this);
        EventManager.instance.Subscribir(eventType.UseKey, this);
        EventManager.instance.Subscribir(eventType.PlayerDied, this);
        EventManager.instance.Subscribir(eventType.GamePaused, this);
        EventManager.instance.Subscribir(eventType.VolumeChanged, this);

        int numeroEscena = SceneManager.GetActiveScene().buildIndex;
        if (numeroEscena == 0) 
        {
            AudioService.instance.PlayMusic("MusicaFondo", true);
        }
        else if (numeroEscena == 1)
        {
            AudioService.instance.PlayMusic("MusicaMenu", true);
        }
    }
    public void OnEvent(IEvent evento)
    {
        switch (evento.Tipo)
        {
            case eventType.CollectiblePicked:
                CollectibleEvent event2 = (CollectibleEvent)evento; //desempaqueta
                if (event2.tipo == CollectibleType.Gema)
                {
                    AudioService.instance.PlaySFX("GemaPickup");
                }
                else if (event2.tipo == CollectibleType.Llaves)
                {
                    AudioService.instance.PlaySFX("KeyPickup");
                }
                else if (event2.tipo == CollectibleType.Corazones)
                {
                    AudioService.instance.PlaySFX("RecogerVida");
                }
                break;

            case eventType.UseKey:
                AudioService.instance.PlaySFX("RomperMuro");
                break;

            case eventType.PlayerDied:
                AudioService.instance.PlaySFX("MagoMuere");
                break;

            case eventType.GamePaused:
                AudioService.instance.PlaySFX("Boton");
                break;
            case eventType.VolumeChanged:
                VolumeEvent event3 = (VolumeEvent)evento;
                SetVolume(event3.volumen);
                break;
        }
    }
    public void SetVolume(float volumen)
    {
        float db = Mathf.Log10(volumen) * 20;
        audioMixer.SetFloat("VolumenGeneral", db);
    }
}
