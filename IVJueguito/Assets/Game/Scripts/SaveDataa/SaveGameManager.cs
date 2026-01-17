using UnityEngine;
using System.Collections.Generic;

public class SaveGameManager : MonoBehaviour
{
    public Transform player;
    public float lives;
    public int gems;
    public int keys;
    public Openable[] openables;

    void Start()
    {
        SaveManager.instance.SetSlot(GameSession.selectedSlot);

        SaveData data = SaveManager.instance.LoadGame();

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

    public void Save()
    {
        SaveData data = new SaveData();

        data.playerPosition = player.position;
        data.playerRotation = player.eulerAngles;
        data.playerLives = lives;
        data.playerGems = gems;
        data.playerKeys = keys;

        data.openablesState = new List<bool>();
        foreach (Openable o in openables)
        {
            data.openablesState.Add(o.IsOpen);
        }
        SaveManager.instance.SaveGame(data);
    }

    public void Load()
    {
        SaveData data = SaveManager.instance.LoadGame();
        if (data == null) return;

        player.position = data.playerPosition;
        player.eulerAngles = data.playerRotation;
        lives = data.playerLives;
        gems = data.playerGems;
        keys = data.playerKeys;

        int count = Mathf.Min(openables.Length, data.openablesState.Count);

        for (int i = 0; i < count; i++)
        {
            openables[i].SetState(data.openablesState[i]);
        }
    }

}
