using UnityEngine;
using System.Collections;

public class MatchMaker : MonoBehaviour
{
    private GameObject player;
    private bool _frisbee = false;
    private PhotonView thisPhotonView;

    private string roomName = "Room01";
    private string roomStatus = "";

    private int maxPlayer = 1;
    private string maxPlayerString = "1";

    private Room[] game;

    private Vector2 scrollPosition;

    private MPSelectScreenFrisbee _selection;
    private bool _selected;
    private GameObject _selectionPrefab;
    private GameObject _courtPrefab;

    private string playerSelection1;
    private string playerSelection2;

    // Use this for initialization
    void Awake()
    {
        _selection = GameObject.FindWithTag("FrisbeeP1").GetComponent<MPSelectScreenFrisbee>();
        _selectionPrefab = GameObject.FindWithTag("SelectionScreen");

    }

    void Start()
    {
        _courtPrefab = GameObject.FindWithTag("Court");
        _courtPrefab.SetActive(false);
        //_selection = GameObject.FindWithTag("FrisbeeP1").GetComponent<MPSelectScreenFrisbee>();
        //PhotonNetwork.logLevel = PhotonLogLevel.Full;
    }

    // Update is called once per frame
    void Update()
    {
        spawnFrisbee();
        if(_selection.SelectedFloat != 0 && !_selected)
        {
            PhotonNetwork.ConnectUsingSettings("0.3");
            _selectionPrefab.SetActive(false);
            _courtPrefab.SetActive(true);
            _selected = true;
            Camera.main.orthographicSize = 10;
            Debug.Log("Connection");
        }
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.insideLobby ==  true)
        {
            GUI.Box(new Rect(Screen.width / 2.5f, Screen.height / 3, 400, 550), "");
            GUILayout.BeginArea(new Rect(Screen.width / 2.5f, Screen.height / 3f, 400, 500));
            GUI.color = Color.red;
            GUILayout.Box("Lobby");
            GUI.color = Color.white;

            GUILayout.Label("Room Name:");
            roomName = GUILayout.TextField("Room Name");
            GUILayout.Label("Max Amount of player 1-20:");
            maxPlayerString = GUILayout.TextField(maxPlayerString, 2);

            if (maxPlayerString != "")
            {
                maxPlayer = int.Parse(maxPlayerString);

                if (maxPlayer > 20)
                    maxPlayer = 20;
                if (maxPlayer == 0)
                    maxPlayer = 1;
            }
            else
            {
                maxPlayer = 1;
            }

            if (GUILayout.Button("Create Room"))
            {
                if (roomName != "" && maxPlayer > 0) // if the room name has a name and max players are larger then 0
                {
                    PhotonNetwork.CreateRoom(roomName, true, true, maxPlayer); // then create a photon room visible , and open with the maxplayers provide by user.

                }
            }

            GUILayout.Space(20);
            GUI.color = Color.red;
            GUILayout.Box("Game Rooms");
            GUI.color = Color.white;
            GUILayout.Space(20);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Width(400), GUILayout.Height(300));

            foreach (RoomInfo game in PhotonNetwork.GetRoomList()) // Each RoomInfo "game" in the amount of games created "rooms" display the fallowing.
            {

                GUI.color = Color.green;
                GUILayout.Box(game.name + " " + game.playerCount + "/" + game.maxPlayers); //Thus we are in a for loop of games rooms display the game.name provide assigned above, playercount, and max players provided. EX 2/20
                GUI.color = Color.white;

                if (GUILayout.Button("Join Room"))
                {

                    PhotonNetwork.JoinRoom(game.name); // Next to each room there is a button to join the listed game.name in the current loop.
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();


        }
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        switch (PhotonNetwork.player.ID)
        {
            case 1:
                switch (_selection.SelectedFloat)
                {
                    case 1:
                        playerSelection1 = "Multiplayer/Online/SinWavePlayer";
                        break;
                    case 2:
                        playerSelection1 = "Multiplayer/Online/DigitalSinPlayer";
                        break;
                    case 3:
                        playerSelection1 = "Multiplayer/Online/SlidePlayer";
                        break;
                    case 4:
                        playerSelection1 = "Multiplayer/Online/SawtoothPlayer";
                        break;
                    case 5:
                        playerSelection1 = "Multiplayer/Online/CirclePlayer";
                        break;
                }
                break;

            case 2:
                switch (_selection.SelectedFloat)
                {
                    case 1:
                        playerSelection2 = "Multiplayer/Online/SinWavePlayerTwo";
                        break;
                    case 2:
                        playerSelection2 = "Multiplayer/Online/DigitalSinPlayerTwo";
                        break;
                    case 3:
                        playerSelection2 = "Multiplayer/Online/SlidePlayerTwo";
                        break;
                    case 4:
                        playerSelection2 = "Multiplayer/Online/SawtoothPlayerTwo";
                        break;
                    case 5:
                        playerSelection2 = "Multiplayer/Online/CirclePlayerTwo";
                        break;
                }
                break;
        }

        if (PhotonNetwork.player.ID == 1)
        {
            player = PhotonNetwork.Instantiate(playerSelection1, new Vector2(-10, 0), Quaternion.identity, 0);
            thisPhotonView = player.GetComponent<PhotonView>();
        }
        else if (PhotonNetwork.player.ID == 2)
        {
            player = PhotonNetwork.Instantiate(playerSelection2, new Vector2(10, 0), Quaternion.identity, 0);
            thisPhotonView = player.GetComponent<PhotonView>();
        }

        if (player.GetComponent<SinWavePlayerOnline>() != null)
        {
            SinWavePlayerOnline controller = player.GetComponent<SinWavePlayerOnline>();
            controller.enabled = true;
        }
        if (player.GetComponent<SinWavePlayerOnlineTwo>() != null)
        {
            SinWavePlayerOnlineTwo controller = player.GetComponent<SinWavePlayerOnlineTwo>();
            controller.enabled = true;
        }

        switch(PhotonNetwork.player.ID)
        {
            case 1:
                player.tag = "PlayerOne";
                Debug.LogError("Player One has entered the game");
                break;
            case 2:
                player.tag = "PlayerTwo";
                Debug.LogError("Player Two has entered the game");
                break;
        }

    }

    [RPC] void spawnFrisbee()
    {
        if (GameObject.FindWithTag("PlayerOne") && GameObject.FindWithTag("PlayerTwo") && !_frisbee)
        {
            if (PhotonNetwork.isMasterClient)
            {
                GameObject frisbee = PhotonNetwork.Instantiate("Multiplayer/Online/OnlineFrisbee", Vector3.zero, Quaternion.identity, 0);
                frisbee.rigidbody2D.AddForce(new Vector2(-1f, 0f));
                _frisbee = true;
            }
        }
    }
}
