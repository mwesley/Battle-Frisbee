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

    private Frisbee _frisbeeScript;

    // Use this for initialization
    void Start()
    {

        p1RoundScore = GameObject.Find("P1Score").guiText;
        p2RoundScore = GameObject.Find("P2Score").guiText;
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
            _frisbeeScript.playerOneWin = false;
            playerOneWin = false;
            Time.timeScale = 0;
        }
        else if (playerTwoWin)
        {
            InstantiateUIPrefab(pOneWin);
            InstantiateUIPrefab(menuButton);
            InstantiateUIPrefab(resetButton);
            InstantiateUIPrefab(exitButton);
            _frisbeeScript.playerTwoWin = false;
            playerTwoWin = false;
            Time.timeScale = 0;
        }

    }
    void LateUpdate()
    {
       /* if (resetting)
        {
            ResetPositions();
            resetting = false;
        }*/
    }

    public void ResetPositions()
    {

        _frisbeeScript.playerCharacter.rigidbody2D.MovePosition(new Vector2(-9, 0));
        _frisbeeScript.AIChar.rigidbody2D.MovePosition(new Vector2(9, 0));
        _frisbeeScript.frisbee.rigidbody2D.MovePosition(new Vector2(0, 0));
        _frisbeeScript.frisbee.rigidbody2D.velocity = Vector2.zero;
        AI.powerValue = 50;
        PlayerControls.powerValue = 50;
        reset = true;
    }

    private void InstantiateUIPrefab(GameObject go)
    {
        go = Instantiate(go, go.transform.position, go.transform.rotation) as GameObject;
        go.transform.SetParent(canvas, false);
    }

}





