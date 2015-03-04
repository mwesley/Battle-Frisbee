using UnityEngine;
using System.Collections;

public class ResetGameButton : MonoBehaviour
{

    public GameObject _selection;
    public GameObject _canvas;
    

    // Use this for initialization
    void Start()
    {
        //Screen.lockCursor = true;
        if (Application.loadedLevelName == "MPStart")
        {
            _selection = GameObject.FindWithTag("SelectionScreen");
            _selection.SetActive(false);
            _canvas = GameObject.Find("Canvas1");
        }
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

    public void SingleplayerButton()
    {
        Application.LoadLevel("SinglePlayer");
    }

    public void ControlButton()
    {
        Application.LoadLevel("ControlScreen");
    }

    public void FindGameButton()
    {
        _canvas.SetActive(false);
        _selection.SetActive(true);
    }
}
