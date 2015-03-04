using UnityEngine;
using System.Collections;

public class NetworkFrisbee : Photon.MonoBehaviour
{
    public OnlineFrisbee FrisbeeScript;
    public GameObject Frisbee;
    private Rigidbody2D _frigidbody;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        Frisbee = this.gameObject;
        FrisbeeScript = Frisbee.GetComponent<OnlineFrisbee>();
        _frigidbody = Frisbee.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            
        }
        else
        {
            syncTime += Time.deltaTime;
            _frigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(_frigidbody.position);
        }
        else
        {
            syncEndPosition = (Vector3)stream.ReceiveNext();
            syncStartPosition = _frigidbody.position;

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;
        }
    }
}

