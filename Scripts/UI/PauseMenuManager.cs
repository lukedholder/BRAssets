using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button restartButton;
    public Button viewCtrlsButton;
    public Button saveQuitButton;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializePauseMenu();
    }

    void InitializePauseMenu()
    {
        isPaused = false;
        // TODO GameObject.Find will probably be taken out later
        pauseMenu = GameObject.Find("PauseMenu");
        resumeButton = GameObject.Find("PauseMenu/PauseMenuButtons/ResumeButton").GetComponent<Button>();
        resumeButton.onClick.AddListener(delegate { Resume(); isPaused = false; });

        restartButton = GameObject.Find("PauseMenu/PauseMenuButtons/RestartButton").GetComponent<Button>();

        viewCtrlsButton = GameObject.Find("PauseMenu/PauseMenuButtons/ViewControlsButton").GetComponent<Button>();
        viewCtrlsButton.onClick.AddListener(delegate {ViewControls(); });

        saveQuitButton = GameObject.Find("PauseMenu/PauseMenuButtons/SaveQuitButton").GetComponent<Button>();
        saveQuitButton.onClick.AddListener(delegate { SaveandQuit(); });

        // make sure game isn't paused when first booting up level
        Resume();
    }

    void Update()
    {
        // if ESC is pressed,
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // game is paused, resume.
                Resume();
            } else
            {
                // game is resumed, pause.
                Pause();
            }

            isPaused = !isPaused;
        }
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("game paused");
    }

    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("game resumed");
    }

    void SaveandQuit()
    {
        // TODO save user data to DB

        // go back to main menu
        SceneManager.LoadScene(0);

    }

    void ViewControls()
    {
        ControlsMenuManager.controlsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Debug.Log("viewing controls");
        
    }

}
