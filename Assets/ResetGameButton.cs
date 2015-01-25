using UnityEngine;
using System.Collections;

public class ResetGameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Screen.lockCursor = true;
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ResetButton()
    {
        Application.LoadLevel("DebugLevel");
        Time.timeScale = 1;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void MenuButton()
    {
        Application.LoadLevel("DebugScene");
    }
}
