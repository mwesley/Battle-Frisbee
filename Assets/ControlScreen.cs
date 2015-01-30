using UnityEngine;
using System.Collections;

public class ControlScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Special"))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
