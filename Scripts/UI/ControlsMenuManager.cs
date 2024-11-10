using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsMenuManager : MonoBehaviour
{
    public static GameObject controlsMenu;
    public Button returnButton;
    public GameObject adminCtrls;
    public GameObject menuToReturnTo;

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
        InitializeControlsMenu();
    }
    void InitializeControlsMenu()
    {
        controlsMenu = GameObject.Find("ViewControlsMenu");
        adminCtrls = GameObject.Find("ViewControlsMenu/AdminControls");
        // TODO check if user is an admin to determine if admin ctrls can be shown
        returnButton = GameObject.Find("ViewControlsMenu/ReturnButton").GetComponent<Button>();
        returnButton.onClick.AddListener(delegate {ReturnFromCtrlMenu(menuToReturnTo);});
        
        // TODO uncomment when login and main menu functionality is integrated
        // if a user is logged in
        // if (GameManager.currentUID > 0)
        // {
        //     // user is playing game, in pause menu
        //     // controls menu should go back to pause menu
        //     menuToReturnTo = GameObject.Find("PauseMenu");
        // } else
        // {
        //     // user is not playing game, in main menu
        //     // controls menu should go back to main menu
        //     menuToReturnTo = GameObject.Find("Main Menu");
        // }

        controlsMenu.SetActive(false);
    }

    void ReturnFromCtrlMenu(GameObject menuToReturnTo)
    {
        menuToReturnTo.SetActive(true);
        controlsMenu.SetActive(false);
    }
}
