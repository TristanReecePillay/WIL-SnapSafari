using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject MiniMapMenuUI;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause(); 
            }
        }
    }

     public void Resume()
    {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1.0f;
            GameIsPaused = false;
            MiniMapMenuUI.SetActive(true);
    }

     public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
        MiniMapMenuUI.SetActive(false);
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu....");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
