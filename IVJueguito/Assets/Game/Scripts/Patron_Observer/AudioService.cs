using System.Collections.Generic;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    public static AudioService instance;
    private void Awake()
    {
        instance = this;
        foreach (Sonidos sonido in listaSonidos)
        {
            libreriaSonidos.Add(sonido.id, sonido);
        }
    }

    public bool isMuted = false;
    public float sfxVolume = 1.0f;
    public float musicVolume =1.0f;

    [System.Serializable] //para que la lista de sonidos aparezca en el inspector
    public class Sonidos
    {
        public string id;
        public AudioClip clip;
        [Range(0f, 1f)] public float volumen = 1f;
    }

    public List<Sonidos> listaSonidos;

    Dictionary<string, Sonidos> libreriaSonidos = new Dictionary<string, Sonidos>();
   
    [SerializeField]private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceMusica;
    public void PlaySFX(string id)
    {
        if (isMuted) return;
        if (libreriaSonidos.ContainsKey(id)){
            Sonidos s = libreriaSonidos[id];
            audioSource.PlayOneShot(s.clip,sfxVolume*s.volumen);
        }
    }
    public void PlayMusic(string id, bool loop)
    {
        if (isMuted) return;
        if(libreriaSonidos.ContainsKey(id))
        {
            Sonidos s = libreriaSonidos[id];
            audioSourceMusica.clip = s.clip;
            audioSourceMusica.loop = loop;
            audioSourceMusica.volume = musicVolume * s.volumen;
            audioSourceMusica.Play();
        }
    }

    public void StopMusic()
    {
        audioSourceMusica.Stop();
    }
}
