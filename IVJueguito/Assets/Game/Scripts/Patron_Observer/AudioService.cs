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
            clips.Add(sonido.id, sonido.clip);
        }
    }

    public bool isMuted = false;
    public float sfxVolume = 1.0f;
    public float musicVolume =1.0f;
    public class Sonidos
    {
        public string id;
        public AudioClip clip;
    }

    public List<Sonidos> listaSonidos;

    Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
   
    [SerializeField]private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceMusica;
    public void PlaySFX(string id)
    {
        if (isMuted) return;
        if (clips.ContainsKey(id)){
            audioSource.PlayOneShot(clips[id],sfxVolume);
        }
    }
    public void PlayMusic(string id, bool loop)
    {
        if (isMuted) return;
        if(clips.ContainsKey(id))
        {
            audioSourceMusica.clip = clips[id];
            audioSourceMusica.loop = loop;
            audioSourceMusica.volume = musicVolume;
            audioSourceMusica.Play();
        }
    }

    public void StopMusic()
    {
        audioSourceMusica.Stop();
    }
}
