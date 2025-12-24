using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public enum eventType //lista de tipos de evento
{
    CollectiblePicked,
    PlayerStatsUpdated,
    PlayerDied,
    LevelStarted,
    LevelCompleted,
    GamePaused,
    AchievementUnlocked
}

public class EventManager : MonoBehaviour
{
    static EventManager instance; //esto se hace porque queremos que sea un singleton(poder acceder desde cualquier sitio)
    

    Dictionary<eventType, List<IObserver> > diccionario  = new Dictionary<eventType, List<IObserver> >();

    EventManager()
    {
        instance = this;
    }

    EventManager GetInstance()
    {
        return instance;
    }

    void Subscribir(eventType tipo, IObserver observer)
    {

    }

    void Dessuscribir(eventType tipo, IObserver observer)
    {

    }

    void Publicar(IEvent evento)
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
