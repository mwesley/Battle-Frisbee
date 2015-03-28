/* Adapted from http://wiki.unity3d.com/index.php?title=KeyCombo
 */
using UnityEngine;
public class KeyCombo
{
    public string[] buttons;
    private int currentIndex = 0; //moves along the array as buttons are pressed

    public float allowedTimeBetweenButtons = 0.5f; //tweak as needed
    private float timeLastButtonPressed;

    public KeyCombo(string[] b)
    {
        buttons = b;
    }

    //usage: call this once a frame. when the combo has been completed, it will return true
    public bool Check()
    {
        if (Time.time > timeLastButtonPressed + allowedTimeBetweenButtons) currentIndex = 0;
        {
            if (currentIndex < buttons.Length)
            {
                if ((buttons[currentIndex] == "down" && Input.GetAxisRaw("Vertical 1") == -1) ||
                (buttons[currentIndex] == "up" && Input.GetAxisRaw("Vertical 1") == 1) ||
                (buttons[currentIndex] == "left" && Input.GetAxisRaw("Horizontal 1") == -1) ||
                (buttons[currentIndex] == "right" && Input.GetAxisRaw("Horizontal 1") == 1) ||
                (buttons[currentIndex] == "down 2" && Input.GetAxisRaw("Vertical 2") == -1) ||
                (buttons[currentIndex] == "up 2" && Input.GetAxisRaw("Vertical 2") == 1) ||
                (buttons[currentIndex] == "left 2" && Input.GetAxisRaw("Horizontal 2") == -1) ||
                (buttons[currentIndex] == "right 2" && Input.GetAxisRaw("Horizontal 2") == 1) ||
                (buttons[currentIndex] != "down" && buttons[currentIndex] != "up" && buttons[currentIndex] != "left" && buttons[currentIndex] != "right" && buttons[currentIndex] != "down 2" && buttons[currentIndex] != "up 2" && buttons[currentIndex] != "left 2" && buttons[currentIndex] != "right 2" && Input.GetButtonDown(buttons[currentIndex])))
                {
                    timeLastButtonPressed = Time.time;
                    currentIndex++;
                }

                if (currentIndex >= buttons.Length)
                {
                    currentIndex = 0;
                    return true;
                }
                else return false;
            }
        }

        return false;
    }
}