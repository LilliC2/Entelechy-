using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : GameBehaviour
{

    public GameObject creditCanvas;
    public GameObject buttonPanels;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        creditCanvas.SetActive(false);
        buttonPanels.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameLoopPrototype");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenCreits()
    {
        creditCanvas.SetActive(true);
        buttonPanels.SetActive(false);
    }

    public void CloseCredits()
    {
        creditCanvas.SetActive(false);
        buttonPanels.SetActive(true);
    }


}
