using UnityEngine;
using System.Collections;

public class MatchMaker : MonoBehaviour
{
    private GameObject player;
    private bool _frisbee = false;
    private PhotonView thisPhotonView;


    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.3");
        //PhotonNetwork.logLevel = PhotonLogLevel.Full;

        
    }

    // Update is called once per frame
    void Update()
    {
        spawnFrisbee();
    }

    void OnGui()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
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
        string playerSelection1 = "Multiplayer/Online/SinWavePlayer";
        string playerSelection2 = "Multiplayer/Online/SinWavePlayerTwo";


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
            Object frisbee = Resources.Load("Multiplayer/Online/OnlineFrisbee");
            Instantiate(frisbee, Vector2.zero, Quaternion.identity);
            _frisbee = true;
        }
    }
}
