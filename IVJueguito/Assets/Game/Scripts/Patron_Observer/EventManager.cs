using System;
using System.Collections.Generic;
using UnityEngine;

public enum eventType //lista de tipos de evento. Fuera de la clase para poder usarlo en otras.
{
    CollectiblePicked,
    PlayerStatsUpdated,
    PlayerDied,
    LevelStarted,
    LevelCompleted,
    GamePaused,
    AchievementUnlocked,
    DialogueStarted
}

public class EventManager : MonoBehaviour
{
    public static EventManager instance; //esto se hace porque queremos que sea un singleton(poder acceder desde cualquier sitio). Y como es public no hace falta un GetInstance
    private void Awake()
    {
        instance = this;
    }

    Dictionary<eventType, List<IObserver> > diccionario  = new Dictionary<eventType, List<IObserver> >();

    public void Subscribir(eventType tipo, IObserver observer)
    {
        if(!diccionario.ContainsKey(tipo)) //en caso de que no exista entrada para ese tipo de evento
        {
            diccionario.Add(tipo, new List<IObserver>()); //creamos una nueva lista de observadores para ese tipo de evento
            //diccionario[tipo] = new List<IObserver>(); otra forma de hacerlo
        }

        //Es otro if porque siempre hay que hacerlo, hubiera previamente lista o no.
        if (!diccionario[tipo].Contains(observer)) //si ya existe la entrada, comprobamos que el observador no este ya suscrito
        {
            diccionario[tipo].Add(observer); //añadimos el observador a la lista de ese tipo de evento
        }
    }

    public void Desuscribir(eventType tipo, IObserver observer)
    {
        if(diccionario.ContainsKey(tipo))
        {
            diccionario[tipo].Remove(observer);

            if(diccionario[tipo].Count == 0) //en caso de que ya no tenga nadie suscrito, eliminamos la entrada del diccionario directamente
            {
                diccionario.Remove(tipo);
            }
        }
    }

    public void Publicar(IEvent evento)
    {
        eventType tipo = evento.Tipo;

        if (diccionario.ContainsKey(tipo))
        {
            foreach (IObserver observer in diccionario[tipo]) //esto es una función muy bonita para recorrer listas 
            {
                observer.OnEvent(evento);
            }
        }
    }
}
