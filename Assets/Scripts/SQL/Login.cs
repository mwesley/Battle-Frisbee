using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    string loginURL = "http://battlefrisbee.hazelhosting.com/login.php";

    string username = "";
    string password = "";
    string label = "";
    float wins;
    float losses;


    private HandleStats _handleStats;

    void Start()
    {
        _handleStats = GameObject.Find("HandleStats").GetComponent<HandleStats>();
    }

    void OnGUI()
    {
        GUI.Window(0, new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2 - 70), LoginWindow, "Login");
    }

    void LoginWindow(int windowID)
    {
        GUI.Label(new Rect(140, 40, 130, 100), "Username:");
        username = GUI.TextField(new Rect(25, 60, 375, 30), username);
        Debug.Log(username);
        GUI.Label(new Rect(140, 92, 130, 100), "Password:");
        password = GUI.PasswordField(new Rect(25, 115, 375, 30), password, '*');

        if(GUI.Button(new Rect(25, 160, 375, 50), "Login"))
        {
            StartCoroutine(HandleLogin(username, password));
        }

        GUI.Label(new Rect(55, 222, 250, 100), label);
    }

    IEnumerator HandleLogin(string username, string password)
    {
        label = "Checking username and password.";
        string loginURL = this.loginURL + "?username=" + username + "&password=" + password;
        WWW loginReader = new WWW(loginURL);
        yield return loginReader;

        Debug.Log(loginReader.text);
        string stats = loginReader.text;
        string[] statStrings = stats.Split('.');

        if(loginReader.error != null)
        {
            label = "Error connecting to database server.";
        }
        else
        {
            if (statStrings[0] == "success")
            {
                label = "Successfully logged in.";
                _handleStats.GetStats(float.Parse(statStrings[1]), float.Parse(statStrings[2]));
                Application.LoadLevel("MPStart");
            }
            else
            {
                label = "Invalid username or password.";
            }
        }
    }

}
