using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public int playerLives;
    public int playerGems;

    public List<bool> openablesState;
}