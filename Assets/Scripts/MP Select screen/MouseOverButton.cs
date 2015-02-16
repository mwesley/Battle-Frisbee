using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseOverButton : MonoBehaviour
{
    public MPSelectScreenFrisbee FrisbeeOne;
    public MPSelectScreenFrisbee FrisbeeTwo;

    private Button thisButton;



    // Use this for initialization
    void Start()
    {
        FrisbeeOne = GameObject.FindWithTag("FrisbeeP1").GetComponent<MPSelectScreenFrisbee>();
        //FrisbeeTwo = GameObject.FindWithTag("FrisbeeP2").GetComponent<MPSelectScreenFrisbee>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D (Collider2D col)
    {

        switch (col.tag)
        {
            case "PlayerOne":
                FrisbeeOne.Highlighted = this.tag;
                if (Input.GetButtonDown("Throw"))
                    FrisbeeOne.Selected = this.tag;
                break;

            case "PlayerTwo":
                FrisbeeTwo.Highlighted = this.tag;
                if (Input.GetButtonDown("Throw 2"))
                    FrisbeeTwo.Selected = this.tag;
                break;
        }

    }

    public void Highlighted()
    {
        
    }

    public void Selected()
    {
        
    }
}
