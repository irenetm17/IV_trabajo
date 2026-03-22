using UnityEngine;
using System.Collections.Generic;

public class SaveGameManager : MonoBehaviour, IObserver
{
    public Transform player;
    public float lives = 3;
    public int gems = 0;
    public int keys = 0;
    public Openable[] openables;

    void Start()
    {
        SaveManager.instance.SetSlot(GameSession.selectedSlot);

        SaveData data = SaveManager.instance.LoadGame();

        EventManager.instance.Subscribir(eventType.PlayerStatsUpdated, this);
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);
        EventManager.instance.Subscribir(eventType.UseKey, this);
        EventManager.instance.Subscribir(eventType.PlayerDied, this);
        EventManager.instance.Subscribir(eventType.GameSaved, this);

        if (data == null)
        {
            // valores por defecto
            lives = 3;
            gems = 0;
            keys = 0;
            return;
        }

        Load();
    }
    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.GameSaved)
        {
            Save();
        }
        if (evento.Tipo == eventType.PlayerStatsUpdated)
        {
            PlayerStatsEvent event2 = (PlayerStatsEvent)evento; //desempaqueta

            lives += event2.health;
            lives = Mathf.Clamp(lives, 0f, 3f);
            gems += event2.gems;
            gems = Mathf.Clamp(gems, 0, 4);
        }

        if (evento.Tipo == eventType.CollectiblePicked)
        {
            CollectibleEvent event4 = (CollectibleEvent)evento; //desempaqueta
            if (event4.tipo == CollectibleType.Corazones)
            {
                lives += event4.amount;
                lives = Mathf.Clamp(lives, 0f, 3f);
            }
            if (event4.tipo == CollectibleType.Llaves)
            {
                keys += event4.amount;
            }
            if (event4.tipo == CollectibleType.Gema)
            {
                gems += event4.amount;
            }
        }
        if (evento.Tipo == eventType.UseKey)
        {
            keys--;
        }

    }

    public void Save()
    {
        SaveData data = new SaveData();

        data.playerPosition = player.position;
        data.playerRotation = player.eulerAngles;
        data.playerLives = lives;
        data.playerGems = gems;
        data.playerKeys = keys;

        Debug.Log("datos guardados:");
        Debug.Log(lives);
        Debug.Log(gems);
        Debug.Log(keys);

        data.openablesState = new List<OpenableState>();
        foreach (Openable o in openables)
        {
            data.openablesState.Add(o.State);
        }
        SaveManager.instance.SaveGame(data);
    }

    public void Load()
    {
        SaveData data = SaveManager.instance.LoadGame();
        if (data == null) return;

        player.position = data.playerPosition;
        player.eulerAngles = data.playerRotation;

        Debug.Log("datos leidos:");
        Debug.Log(data.playerLives);
        Debug.Log(data.playerGems);
        Debug.Log(data.playerKeys);

        CollectibleEvent collectibleEvent = new CollectibleEvent(CollectibleType.Llaves, data.playerKeys);
        EventManager.instance.Publicar(collectibleEvent);

        PlayerStatsEvent vidasRestar = new PlayerStatsEvent((data.playerLives - 3.0f), data.playerGems);
        EventManager.instance.Publicar(vidasRestar);

        int count = Mathf.Min(openables.Length, data.openablesState.Count);

        for (int i = 0; i < count; i++)
        {
            openables[i].SetState(data.openablesState[i]);
        }
        // Buscamos todas las puertas que necesiten llave en la escena y les actualizamos su contador interno
        /*LlaveInteractuar[] puertasConLlave = FindObjectsOfType<LlaveInteractuar>();
        foreach (var p in puertasConLlave)
        {
            // Esto es un "truco" para sincronizar el contador de llaves interno de cada script 
            // con el valor global que acabamos de cargar.
            p.SetLlavesInternas(data.playerKeys);
        }*/
    }
    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.PlayerStatsUpdated, this);
            EventManager.instance.Desuscribir(eventType.CollectiblePicked, this);
            EventManager.instance.Desuscribir(eventType.UseKey, this);
            EventManager.instance.Desuscribir(eventType.PlayerDied, this);
            EventManager.instance.Desuscribir(eventType.GameSaved, this);
        }
    }
}
