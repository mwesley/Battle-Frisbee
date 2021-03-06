﻿using UnityEngine;
using System.Collections;

public class OnlineFrisbee : Photon.MonoBehaviour
{

    public PhotonView thisPhotonView;

    private GameObject _frisbee;
    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    public bool caught = false;
    public bool PlayerOneCaught = false;
    public bool PlayerTwoCaught = false;

    public bool playerOneWin = false;
    public bool playerTwoWin = false;

    public GameObject MultiplayerFrisbee;

    private float timer = 0f;

    private Vector2 frisbeeVelocity;

    private GameObject _wall;

    //private MPScoring _scoreScript;
    public MultiplayerPlayerOnline _playerOneScript;
    public MultiplayerPlayerOnlineTwo _playerTwoScript;

    private bool _practice;
    private bool _gameOn;
    private bool _playerTwoExists = false;

    // Use this for initialization
    void Start()
    {
        thisPhotonView = this.gameObject.GetComponent<PhotonView>();

        _frisbee = GameObject.FindWithTag("frisbee");

        playerOneWin = false;
        playerTwoWin = false;
        PlayerOneCaught = false;
        PlayerTwoCaught = false;
        caught = false;

        _wall = GameObject.FindGameObjectWithTag("Wall");

        //_scoreScript = (GameObject.FindWithTag("MainCamera")).GetComponent<MPScoring>();

        if (!PlayerOne || !PlayerTwo || !_frisbee)
        {
            PlayerOne = GameObject.FindWithTag("PlayerOne");
            PlayerTwo = GameObject.FindWithTag("PlayerTwo");
            _frisbee = GameObject.FindWithTag("frisbee");
        }

        if (PlayerOne)
        {
            _playerOneScript = PlayerOne.GetComponent<MultiplayerPlayerOnline>();
            _playerOneScript.frisbee = this.gameObject;
            _playerOneScript._FrisbeeScript = this.gameObject.GetComponent<OnlineFrisbee>();
            Debug.Log("PlayerOne has entered");
        }
        if (PlayerTwo)
        {
            _playerTwoScript = PlayerTwo.GetComponent<MultiplayerPlayerOnlineTwo>();
            _playerTwoScript.frisbee = this.gameObject;
            _playerTwoScript._FrisbeeScript = this.gameObject.GetComponent<OnlineFrisbee>();
            Debug.Log("PlayerTwo has entered");
        }

        rigidbody2D.AddForce(new Vector2(-1f, 0f));

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if(!PlayerTwo)
        {
            PlayerTwo = GameObject.FindWithTag("PlayerTwo");

        }
        else if (PlayerTwo && !_playerTwoExists)
        {
            _playerTwoScript = PlayerTwo.GetComponent<MultiplayerPlayerOnlineTwo>();
            _playerTwoScript.frisbee = this.gameObject;
            _playerTwoScript._FrisbeeScript = this.gameObject.GetComponent<OnlineFrisbee>();
            Debug.Log("PlayerTwo has entered");
            _playerTwoExists = true;
        }

        float x = rigidbody2D.velocity.x;
        float y = rigidbody2D.velocity.y;
        float z = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(90.0f - z, Vector3.forward);

        if (PlayerOneCaught)
        {
            _frisbee.transform.position = new Vector2(PlayerOne.transform.position.x +1.5f, PlayerOne.transform.position.y);
            _playerTwoScript.special = false;
            _playerTwoScript.bezierFlight = false;
        }

        if(PlayerTwoCaught)
        {
            _frisbee.transform.position = new Vector2(PlayerTwo.transform.position.x - 1.5f, PlayerTwo.transform.position.y);
            _playerOneScript.special = false;
            _playerOneScript.bezierFlight = false;
        }


        if (PlayerOne && !_practice && !_gameOn)
        {
            frisbeeVelocity = new Vector2(-5.0f, 0f);
            rigidbody2D.AddForce(frisbeeVelocity);
            _practice = true;
        }

        if (PlayerOne && PlayerTwo && _practice)
        {
            _practice = false;
            _gameOn = true;
        }

        if (PlayerOneCaught || PlayerTwoCaught)
        {
            caught = true;
        }
        else if (!PlayerOneCaught || !PlayerTwoCaught)
        {
            caught = false;
        }

       /* if (_scoreScript.reset)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                _frisbee.rigidbody2D.AddForce(frisbeeVelocity);
                timer = 0f;
                _scoreScript.reset = false;
            }
        }*/

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        switch (col.gameObject.tag)
        {
            case "PlayerOne":

                //_frisbee.transform.SetParent(PlayerOne.transform);
                PlayerOneCaught = true;
                //_frisbee.transform.localPosition = new Vector2(0f, -0.2f);
                rigidbody2D.velocity = new Vector2(0, 0);
                _playerTwoScript.special = false;
                _playerTwoScript.bezierFlight = false;

                break;

            case "PlayerTwo":


                this.gameObject.GetComponent<PhotonView>().RPC("ParentSetPlayerTwo", PhotonTargets.All);

                break;

           /* case "ScoreWallP1":
            case "ScoreWallP2":

                _playerOneScript.special = false;
                _playerOneScript.bezierFlight = false;
                _playerTwoScript.special = false;
                _playerTwoScript.bezierFlight = false;
                Score(col.gameObject.tag);
                _scoreScript.ResetPositions();
                if (_scoreScript.playerOneRoundScore >= 5)
                    _scoreScript.playerOneWin = true;
                else if (_scoreScript.playerTwoRoundScore >= 5)
                    _scoreScript.playerTwoWin = true;



                break;*/

            case "Wall":

                _playerOneScript.bezierFlight = false;
                _playerTwoScript.bezierFlight = false;

                if (SidlePlayerOne.sidle)
                {
                    _frisbee.rigidbody2D.velocity = new Vector2(30, 0);
                }
                else if (SidlePlayerTwo.sidle)
                {
                    _frisbee.rigidbody2D.velocity = new Vector2(-30, 0);
                }

                break;

        }
    }


   /* void Score(string player)
    {

        switch (player)
        {
            case "ScoreWallP1":

                _scoreScript.playerTwoRoundScore++;
                _scoreScript.p2RoundScore.text = "Score: " + _scoreScript.playerTwoRoundScore;

                break;

            case "ScoreWallP2":

                _scoreScript.playerOneRoundScore++;
                _scoreScript.p1RoundScore.text = "Score: " + _scoreScript.playerOneRoundScore;

                break;
        }
    }*/

    [RPC]
    private void ParentSetPlayerOne()
    {
        this.transform.SetParent(PlayerOne.transform);
    }

    [RPC]
    private void ParentSetPlayerTwo()
    {
        //_frisbee.transform.SetParent(PlayerTwo.transform);
        PlayerTwoCaught = true;
       // _frisbee.transform.localPosition = new Vector2(0.0f, -0.2f);
        rigidbody2D.velocity = new Vector2(0, 0);
        _playerOneScript.special = false;
        _playerOneScript.bezierFlight = false;
    }
}
