using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class PlayerData
{

    public int level;
    public int health;
    public float[] position;

    public PlayerData (NewMovement player)
    {
        level = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(level);
        health = player.currentHealth;
        Debug.Log(health);

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        Debug.Log(position);

    }

}
