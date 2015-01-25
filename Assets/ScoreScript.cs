using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour
{
    public static float playerOneRoundScore;
    public static float playerTwoRoundScore;

    public static GUIText p1RoundScore;
    public static GUIText p2RoundScore;

    public static GameObject frisbee;

    public static bool gameEnd;
    public static bool reset = false;

    public GameObject pOneWin;
    public GameObject pTwoWin;
    public GameObject resetButton;
    public GameObject exitButton;
    public GameObject menuButton;


    public static bool playerOneWin = false;
    public static bool playerTwoWin = false;

    public static bool resetting;

    public Transform canvas;

    // Use this for initialization
    void Start()
    {
        frisbee = GameObject.FindWithTag("frisbee");
        p1RoundScore.text = "Score: 0";
        p2RoundScore.text = "Score: 0";
        gameEnd = false;
        reset = false;
        playerOneRoundScore = 0f;
        playerTwoRoundScore = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (playerOneWin)
        {
            InstantiateUIPrefab(pOneWin);
            InstantiateUIPrefab(menuButton);
            InstantiateUIPrefab(resetButton);
            InstantiateUIPrefab(exitButton);
            Frisbee.playerOneWin = false;
            playerOneWin = false;
            Time.timeScale = 0;
        }
        else if (playerTwoWin)
        {
            InstantiateUIPrefab(pOneWin);
            InstantiateUIPrefab(menuButton);
            InstantiateUIPrefab(resetButton);
            InstantiateUIPrefab(exitButton);
            Frisbee.playerTwoWin = false;
            playerTwoWin = false;
            Time.timeScale = 0;
        }

    }
    void LateUpdate()
    {
        if (resetting)
        {
            ResetPositions();
            resetting = false;
        }
    }

    public static void ResetPositions()
    {

        Frisbee.playerCharacter.rigidbody2D.MovePosition(new Vector2(-9, 0));
        Frisbee.AIChar.rigidbody2D.MovePosition(new Vector2(9, 0));
        Frisbee.frisbee.rigidbody2D.MovePosition(new Vector2(0, 0));
        Frisbee.frisbee.rigidbody2D.velocity = Vector2.zero;
        reset = true;
    }

    private void InstantiateUIPrefab(GameObject go)
    {
        go = Instantiate(go, go.transform.position, go.transform.rotation) as GameObject;
        go.transform.SetParent(canvas, false);
    }

}





