using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int currentHealth;
    private int sceneLoader;
    public void NewGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        data.health = 0;
        data.level = 0;
        data.position[0] = 0;
        data.position[1] = 0;
        data.position[2] = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadPlayer ()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentHealth = data.health;
        Debug.Log(currentHealth);
        sceneLoader = data.level;
        Debug.Log(sceneLoader);
        SceneManager.LoadScene(sceneLoader);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        //transform.position = position;
        Debug.Log(position);
    }

}
