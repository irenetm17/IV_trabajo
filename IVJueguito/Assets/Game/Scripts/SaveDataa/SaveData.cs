using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public float playerLives;
    public int playerGems;
    public int playerKeys;

    public List<bool> openablesState;
}