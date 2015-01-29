using UnityEngine;
using System.Collections;

public class ResetGameButton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //Screen.lockCursor = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetButton()
    {
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1;
    }

    public void MultiplayerButton()
    {
        Application.LoadLevel("MPSelectionScreen");
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
