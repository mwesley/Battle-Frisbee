﻿using UnityEngine;
using System.Collections;

public class PlayerSelections : MonoBehaviour
{

    public MPSelectScreenFrisbee Player1;
    public MPSelectScreenFrisbee Player2;

    private static int _playerOneAbility;
    private static int _playerTwoAbility;

    private GameObject _PlayerOne;
    private GameObject _PlayerTwo;
    private GameObject _frisbee;

    public GameObject SinWavePlayer;
    public GameObject DigitalSinPlayer;
    public GameObject SlidePlayer;
    public GameObject SawtoothPlayer;
    public GameObject CirclePlayer;

    public GameObject frisbee;

    // Use this for initialization
    void Start()
    {
        
        if (Application.loadedLevelName == "MPCourt")
        {
            _frisbee = Instantiate(frisbee, new Vector2(0, 0), Quaternion.identity) as GameObject;
            InstantiatePlayerOne();
            _PlayerOne.tag = "PlayerOne";
            InstantiatePlayerTwo();
            _PlayerTwo.tag = "PlayerTwo";
            Debug.Log("Loaded?");
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName != "MPCourt")
        {
            _playerOneAbility = Player1.SelectedFloat;
            _playerTwoAbility = Player2.SelectedFloat;
            if (Input.GetButtonDown("Throw"))
            {
                if (_playerOneAbility != 0 && _playerTwoAbility != 0)
                {
                    Application.LoadLevel("MPCourt");
                }
            }
        }


    }

    void InstantiatePlayerOne()
    {
        switch (_playerOneAbility)
        {
            case 1:

                _PlayerOne = Instantiate(SinWavePlayer, new Vector2(-9, 0), Quaternion.identity) as GameObject;

                break;

            case 2:

                _PlayerOne = Instantiate(DigitalSinPlayer, new Vector2(-9, 0), Quaternion.identity) as GameObject;

                break;

            case 3:

                _PlayerOne = Instantiate(SlidePlayer, new Vector2(-9, 0), Quaternion.identity) as GameObject;

                break;

            case 4:

                _PlayerOne = Instantiate(SawtoothPlayer, new Vector2(-9, 0), Quaternion.identity) as GameObject;

                break;

            case 5:

                _PlayerOne = Instantiate(CirclePlayer, new Vector2(-9, 0), Quaternion.identity) as GameObject;

                break;

        }
    }

    void InstantiatePlayerTwo()
    {
        switch (_playerTwoAbility)
        {
            case 1:

                _PlayerTwo = Instantiate(SinWavePlayer, new Vector2(9, 0), Quaternion.identity) as GameObject;

                break;

            case 2:

                _PlayerTwo = Instantiate(DigitalSinPlayer, new Vector2(9, 0), Quaternion.identity) as GameObject;

                break;

            case 3:

                _PlayerTwo = Instantiate(SlidePlayer, new Vector2(9, 0), Quaternion.identity) as GameObject;

                break;

            case 4:

                _PlayerTwo = Instantiate(SawtoothPlayer, new Vector2(9, 0), Quaternion.identity) as GameObject;

                break;

            case 5:

                _PlayerTwo = Instantiate(CirclePlayer, new Vector2(9, 0), Quaternion.identity) as GameObject;

                break;

        }
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            _frisbee = Instantiate(frisbee, new Vector2(0, 0), Quaternion.identity) as GameObject;
            InstantiatePlayerOne();
            _PlayerOne.tag = "PlayerOne";
            InstantiatePlayerTwo();
            _PlayerTwo.tag = "PlayerTwo";
            Debug.Log("Loaded?");
        }
    }
}
