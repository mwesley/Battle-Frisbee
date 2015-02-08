using UnityEngine;
using System.Collections;

public class MPScoring : MonoBehaviour
{
    public float playerOneRoundScore;
    public float playerTwoRoundScore;

    public GUIText p1RoundScore;
    public GUIText p2RoundScore;

    private GameObject _frisbee;

    public bool gameEnd;
    public bool reset = false;

    public GameObject pOneWin;
    public GameObject pTwoWin;
    public GameObject resetButton;
    public GameObject exitButton;
    public GameObject menuButton;


    public bool playerOneWin = false;
    public bool playerTwoWin = false;

    public bool resetting;

    public Transform canvas;

    public MPFrisbee FrisbeeScript;

    // Use this for initialization
    void Start()
    {

       // p1RoundScore = GameObject.Find("P1Score").guiText;
       // p2RoundScore = GameObject.Find("P2Score").guiText;
        _frisbee = GameObject.FindWithTag("frisbee");
        p1RoundScore.text = "Score: 0";
        p2RoundScore.text = "Score: 0";
        gameEnd = false;
        reset = false;
        playerOneRoundScore = 0f;
        playerTwoRoundScore = 0f;

        FrisbeeScript = _frisbee.GetComponent<MPFrisbee>();
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
            FrisbeeScript.playerOneWin = false;
            playerOneWin = false;
            Time.timeScale = 0;
        }
        else if (playerTwoWin)
        {
            InstantiateUIPrefab(pTwoWin);
            InstantiateUIPrefab(menuButton);
            InstantiateUIPrefab(resetButton);
            InstantiateUIPrefab(exitButton);
            FrisbeeScript.playerTwoWin = false;
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

        FrisbeeScript.PlayerOne.rigidbody2D.MovePosition(new Vector2(-9, 0));
        FrisbeeScript.PlayerTwo.rigidbody2D.MovePosition(new Vector2(9, 0));
        _frisbee.rigidbody2D.MovePosition(new Vector2(0, 0));
        _frisbee.rigidbody2D.velocity = Vector2.zero;
        
        reset = true;
    }

    private void InstantiateUIPrefab(GameObject go)
    {
        go = Instantiate(go, go.transform.position, go.transform.rotation) as GameObject;
        go.transform.SetParent(canvas, false);
    }

}





