using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LoginMenuManager : MonoBehaviour
{
    public GameObject loginMenu;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI loginHeader;
    public TextMeshProUGUI loginAlert;
    public Button createAcctButton;
    public Button loginButton;
    public Button registerButton;
    public TextMeshProUGUI highScoreList;
    private bool isCreatingAccount;

    void Start()
    {
        loginMenu = GameObject.Find("Login Menu");

        if (loginMenu == null)
        {
            Debug.Log("login menu not found");
            return;
        }

        // TODO GameObject.Find will probably be taken out later
        loginHeader = GameObject.Find("Login Menu/LoginPrompt/Header").GetComponent<TextMeshProUGUI>();
        loginAlert = GameObject.Find("Login Menu/LoginPrompt/LoginAlert").GetComponent<TextMeshProUGUI>();
        loginAlert.SetText("");

        usernameInput = GameObject.Find("Login Menu/LoginPrompt/UsernameInput").GetComponent<TMP_InputField>();
        passwordInput = GameObject.Find("Login Menu/LoginPrompt/PasswordInput").GetComponent<TMP_InputField>();

        // make sure input is checked
        usernameInput.onValueChanged.AddListener(delegate { CheckInput(usernameInput); });
        passwordInput.onValueChanged.AddListener(delegate { CheckInput(passwordInput); });

        loginButton = GameObject.Find("Login Menu/LoginPrompt/LoginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(delegate {LogInUser();});

        createAcctButton = GameObject.Find("Login Menu/LoginPrompt/CreateAcctButton").GetComponent<Button>();
        createAcctButton.onClick.AddListener(delegate {ToggleLoginPrompt();});

        registerButton = GameObject.Find("Login Menu/LoginPrompt/RegisterButton").GetComponent<Button>();
        registerButton.onClick.AddListener(delegate {RegisterUser();});

        highScoreList = GameObject.Find("Login Menu/Scoreboard/HighScoreList").GetComponent<TextMeshProUGUI>();
        
        // if the user is logged in
        if (GameManager.currentUID < 0)
        {
            // user is not logged in
            SetUpLoginMenu();
        } else 
        {
            // user is logged in
            // hide the login menu to just display main menu
            loginMenu.SetActive(false);
        }
    }

    void SetUpLoginMenu()
    {
        // create DB if it doesn't already exist
        UserDB.db.CreateDB();

        // set all login UI parts to default states
        isCreatingAccount = false;
        usernameInput.text = "";
        passwordInput.text = "";
        loginAlert.SetText("");
        loginMenu.SetActive(true);
        DisplayScoreboard();
    }

    void CheckInput(TMP_InputField inputField)
    {
        // make sure user cannot input spaces or special characters in their username or password.
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Replace(" ", "");
            inputField.text = inputField.text.Replace("'", "");
            inputField.text = inputField.text.Replace("%", "");
            inputField.text = inputField.text.Replace("=", "");
            inputField.text = inputField.text.Replace("!", "");
            inputField.text = inputField.text.Replace(";", "");
            inputField.text = inputField.text.Replace(",", "");
        }

        if (inputField.text.Length > 20)
        {
            // make sure the user cannot enter text longer than 20 chars
            inputField.text = inputField.text[..20];
        }
    }

    void Update()
    {
        if (GameManager.currentUID < 0)
        {
            // user is not logged in
            // if the login menu currently isn't active,
            if (!loginMenu.activeSelf)
            {
                // activate it
                SetUpLoginMenu();
            }

        } else
        {
            // user is logged in
            // hide the login menu
            loginMenu.SetActive(false);
        }
    }

    public void ToggleLoginPrompt(string alert = "")
    {
        // show the given alert
        loginAlert.SetText(alert);
        isCreatingAccount = !isCreatingAccount;
        // toggle login button to reveal/hide register button
        loginButton.gameObject.SetActive(!isCreatingAccount);

        if (isCreatingAccount)
        {
            // change login prompt header
            loginHeader.SetText("Register");
            createAcctButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Back");

        } else {
            loginHeader.SetText("Log In");
            createAcctButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Create An Account");
        }

    }

    public void RegisterUser() {

        // // get input for username and password
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (UserDB.db.UserExists(username) <= 0 && UserDB.db.AddUser(username, password))
        {
            ToggleLoginPrompt("Registered " + username + "! Please log in.");
        } else
        {
            loginAlert.SetText("Issue registering. Try again!");
        }
    }

    public void LogInUser()
    {
        // get input for username and password
        string username = usernameInput.text;
        string password = passwordInput.text;
        int uid = UserDB.db.UserExists(username);

        if (uid > 0 && UserDB.db.PasswordCorrect(username, password))
        {
            // log the user in
            GameManager.currentUID = uid;
            // go to main menu (located underneath login menu)
            loginMenu.SetActive(false);
            Debug.Log("Logged in "+ username);
        } else 
        {
            // notify user that their inputs are wrong
            loginAlert.SetText("Invalid login information.");
        }

    }

    void DisplayScoreboard()
    {
        // update the scoreboard text
        highScoreList.SetText(UserDB.db.GetScoreboardData());
    }
}
