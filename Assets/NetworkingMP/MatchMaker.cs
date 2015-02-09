using UnityEngine;
using System.Collections;

public class MatchMaker : MonoBehaviour
{
    private GameObject player;
    private bool _frisbee = false;


    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.3");
        //PhotonNetwork.logLevel = PhotonLogLevel.Full;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("PlayerTwo") && !_frisbee)
        {
            PhotonNetwork.Instantiate("Multiplayer/Online/OnlineFrisbee", Vector3.zero, Quaternion.identity, 0);
            _frisbee = true;
        }
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
        string playerSelection = "Multiplayer/Online/SinWavePlayer";


        if (PhotonNetwork.player.ID == 1)
        {
            player = PhotonNetwork.Instantiate(playerSelection, new Vector2(-10, 0), Quaternion.identity, 0);
        }
        else if (PhotonNetwork.player.ID == 2)
        {
            player = PhotonNetwork.Instantiate(playerSelection, new Vector2(10, 0), Quaternion.identity, 0);

        }

        if (player.GetComponent<SinWavePlayerOnline>() != null)
        {
            SinWavePlayerOnline controller = player.GetComponent<SinWavePlayerOnline>();
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
}
