using UnityEngine;
using System.Collections.Generic;

public class SaveGameManager : MonoBehaviour
{
    public Transform player;
    public int lives;
    public int gems;
    public Openable[] openables;


    public void Save()
    {
        SaveData data = new SaveData();

        data.playerPosition = player.position;
        data.playerRotation = player.eulerAngles;
        data.playerLives = lives;
        data.playerGems = gems;

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

        int count = Mathf.Min(openables.Length, data.openablesState.Count);

        for (int i = 0; i < count; i++)
        {
            openables[i].SetState(data.openablesState[i]);
        }
    }
}
