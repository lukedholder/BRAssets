using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public Button newGameButton;
    public Button continueButton;
    public Button viewCtrlsButton;
    public Button logoutButton;

    void Start()
    {
        mainMenu = GameObject.Find("Main Menu");

        // TODO GameObject.Find will probably be removed later
        newGameButton = GameObject.Find("Main Menu/MainMenuButtons/NewGameButton").GetComponent<Button>();
        newGameButton.onClick.AddListener(delegate {NewGame();});

        continueButton = GameObject.Find("Main Menu/MainMenuButtons/ContinueButton").GetComponent<Button>();
        continueButton.onClick.AddListener(delegate {NewGame();}); // TODO change to continue

        viewCtrlsButton = GameObject.Find("Main Menu/MainMenuButtons/ViewControlsButton").GetComponent<Button>();
        viewCtrlsButton.onClick.AddListener(delegate {ViewControls();});

        logoutButton = GameObject.Find("Main Menu/MainMenuButtons/LogoutButton").GetComponent<Button>();
        logoutButton.onClick.AddListener(delegate {LogOut();});
    }

    void ViewControls()
    {
        ControlsMenuManager.controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
        Debug.Log("viewing controls");
        
    }

    public void NewGame()
    {
        // TODO wipe any previous save data to be the starter data
        
        // go to gameplay scene
        SceneManager.LoadScene(1);
        // TODO to be remove
        gameObject.GetComponent<LoginMenuManager>().enabled = false;
        gameObject.GetComponent<MainMenuManager>().enabled = false;
        gameObject.GetComponent<PauseMenuManager>().enabled = true;
    }

    public void LogOut()
    {
        // update game manager stored current user
        GameManager.currentUID = -1;
        Debug.Log("logged out");
    }
}
