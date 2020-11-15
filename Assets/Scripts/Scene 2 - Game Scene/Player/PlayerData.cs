using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public PlayerStats playerStats;
    public int level;
    public int health;
    public float[] position; // = Vector3
    public PlayerData(Player player)

    {
        level = playerStats.level;
        health = (int)playerStats.CurrentHealth;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}