using UnityEngine;

public class DialogueStartedEvent : IEvent
{
    public eventType Tipo
    {
        get
        {
            return eventType.DialogueStarted;
        }
    }

    public string[] arrayTextos;
    public DialogueStartedEvent(string[] array)
    {
        this.arrayTextos = array;
    }
}
