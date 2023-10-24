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

    }

    public void Respawn()
    {
        
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
