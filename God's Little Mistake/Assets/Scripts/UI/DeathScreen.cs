using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : GameBehaviour<DeathScreen> 
{
    public GameObject DeathScreenPanel;
    public string currentScene;


    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if(_PC.health <= 0)
        {
            Time.timeScale = 0f;
            DeathScreenPanel.SetActive(true);
            
        }
        else DeathScreenPanel.SetActive(false);

    }

    public void Respawn()
    {
        _GM.Restart();
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
