using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Button resumeButton;
    private UnityEngine.EventSystems.EventSystem esc;
    
    void Start()
    {
        pauseMenuUI = GameObject.Find("Canvas/PauseMenu");
        pauseMenuUI.SetActive(false);
        resumeButton = pauseMenuUI.transform.GetChild(0).GetComponent<Button>();
        esc = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(GameIsPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        esc.SetSelectedGameObject(null);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        resumeButton.Select();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;  Namespace error
    }

}
