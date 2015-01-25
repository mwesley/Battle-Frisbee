using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseOverButton : MonoBehaviour
{
    public MPSelectScreenFrisbee thisScript;

    private Button thisButton;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Highlighted()
    {
        thisScript.Highlighted = this.gameObject.tag;
    }

    public void Selected()
    {
        thisScript.Selected = this.gameObject.tag;
        thisButton = GetComponent<Button>();
        thisButton.interactable = false;
        
    }
}
