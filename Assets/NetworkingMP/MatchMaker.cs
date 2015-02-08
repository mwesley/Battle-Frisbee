using UnityEngine;
using System.Collections;

public class MatchMaker : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.3");
        //PhotonNetwork.logLevel = PhotonLogLevel.Full;
    }

    // Update is called once per frame
    void Update()
    {

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

        GameObject player = PhotonNetwork.Instantiate(playerSelection, Vector3.zero, Quaternion.identity, 0);

        if (player.GetComponent<SinWavePlayerOnline>() != null)
        {
            SinWavePlayerOnline controller = player.GetComponent<SinWavePlayerOnline>();
            controller.enabled = true;
        }
        if (!GameObject.FindWithTag("PlayerOne"))
        {
            player.gameObject.tag = "PlayerOne";
        }
        else
        {
            player.gameObject.tag = "PlayerTwo";
        }

    }
}
