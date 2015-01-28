using UnityEngine;
using System.Collections;

public class MPFrisbee : MonoBehaviour
{


    private GameObject _frisbee;
    private GameObject _PlayerOne;
    private GameObject _PlayerTwo;
    public bool caught;
    public bool PlayerOneCaught = false;
    public bool PlayerTwoCaught = false;

    public bool playerOneWin = false;
    public bool playerTwoWin = false;

    public GameObject MultiplayerFrisbee;

    private float timer = 0f;

    private Vector2 frisbeeVelocity;

    private GameObject _wall;

    // Use this for initialization
    void Start()
    {
        frisbeeVelocity = new Vector2(-5.0f, 0f);
        rigidbody2D.AddForce(frisbeeVelocity);

        _PlayerOne = GameObject.FindWithTag("PlayerOne");
        _PlayerTwo = GameObject.FindWithTag("PlayerTwo");
        _frisbee = GameObject.FindWithTag("frisbee");

        playerOneWin = false;
        playerTwoWin = false;
        PlayerOneCaught = false;
        PlayerTwoCaught = false;
        caught = false;

        _wall = GameObject.FindGameObjectWithTag("Wall");
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (ScoreScript.reset == true)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                _frisbee.rigidbody2D.AddForce(frisbeeVelocity);
                ScoreScript.reset = false;
                timer = 0f;
            }
        }

        float x = rigidbody2D.velocity.x;
        float y = rigidbody2D.velocity.y;
        float z = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(90.0f - z, Vector3.forward);

        if (playerOneWin)
        {
            ScoreScript.playerOneWin = true;
        }
        if (playerTwoWin)
        {
            ScoreScript.playerTwoWin = true;
        }

        if(!_PlayerOne || !_PlayerTwo || !_frisbee)
        {
        _PlayerOne = GameObject.FindWithTag("PlayerOne");
        _PlayerTwo = GameObject.FindWithTag("PlayerTwo");
        _frisbee = GameObject.FindWithTag("frisbee");
        }

        if(PlayerOneCaught || PlayerTwoCaught)
        {
            caught = true;
        }
        else if (!PlayerOneCaught || !PlayerTwoCaught)
        {
            caught = false;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerOne")
        {
            PlayerControls.special = false;
            PlayerControls.bezierFlight = false;
            _frisbee.transform.SetParent(_PlayerOne.transform);
            PlayerOneCaught = true;
            _frisbee.transform.localPosition = new Vector2(0.0f, 0.0f);
            rigidbody2D.velocity = new Vector2(0, 0);
            PlayerControls.powerValue += 10;
        }

        if (col.gameObject.tag == "PlayerTwo")
        {
            PlayerControls.special = false;
            PlayerControls.bezierFlight = false;
            _frisbee.transform.parent = _PlayerTwo.transform;
            PlayerTwoCaught = true;
            _frisbee.transform.localPosition = new Vector2(0.0f, 0.0f);
            rigidbody2D.velocity = new Vector2(0, 0);
            PlayerControls.powerValue += 10;
        }


        if (PlayerControls.bezierFlight ||  PlayerControls.special)
        {
            if (col.gameObject.tag == "Wall" && !SidleAlongWall.sidle)
            {
                rigidbody2D.velocity = -transform.position.normalized * 15;
            }
            else if (col.gameObject.tag == "Wall" && SidleAlongWall.sidle)
            {
                rigidbody2D.velocity = new Vector2(20, 0);
                PlayerControls.special = false;
            }
        }

        if (col.gameObject.tag == "ScoreWallP1")
        {
            PlayerControls.special = false;
            PlayerControls.bezierFlight = false;
            ScoreScript.resetting = true;
            ScoreScript.playerTwoRoundScore++;
            ScoreScript.p2RoundScore.text = "Score: " + ScoreScript.playerTwoRoundScore;

        }

        if (col.gameObject.tag == "ScoreWallP2")
        {
            PlayerControls.special = false;
            PlayerControls.bezierFlight = false;
            ScoreScript.resetting = true;
            ScoreScript.playerOneRoundScore++;
            ScoreScript.p1RoundScore.text = "Score: " + ScoreScript.playerOneRoundScore;
        }

        if (ScoreScript.playerOneRoundScore == 5)
        {
            Debug.Log("Player one wins!");
            ScoreScript.gameEnd = true;
            playerOneWin = true;
        }

        if (ScoreScript.playerTwoRoundScore == 5)
        {
            Debug.Log("Player two wins!");
            ScoreScript.gameEnd = true;
            playerTwoWin = true;
        }

        PlayerControls.bezierFlight = false;
    }



}
