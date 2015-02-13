using UnityEngine;
using System.Collections;

public class NetworkFrisbee : Photon.MonoBehaviour
{
    private Vector3 correctFrisbeePos;
    private Vector2 correctRigidPos;
    private Vector2 correctRigidVel;

    public OnlineFrisbee FrisbeeScript;
    public GameObject Frisbee;


    // Use this for initialization
    void Start()
    {
        Frisbee = this.gameObject;
        FrisbeeScript = Frisbee.GetComponent<OnlineFrisbee>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctFrisbeePos, Time.deltaTime * 9);
            rigidbody2D.position = Vector2.Lerp(rigidbody2D.position, this.correctRigidPos, Time.deltaTime * 9);
            rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity, this.correctRigidVel, Time.deltaTime * 9);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(FrisbeeScript.PlayerOneCaught);
            stream.SendNext(FrisbeeScript.PlayerTwoCaught);
            stream.SendNext(FrisbeeScript.caught);

            stream.SendNext(rigidbody2D.position);
            stream.SendNext(rigidbody2D.velocity);

            
        }
        else
        {
            this.correctFrisbeePos = (Vector3)stream.ReceiveNext();
            this.FrisbeeScript.PlayerOneCaught = (bool)stream.ReceiveNext();
            this.FrisbeeScript.PlayerTwoCaught = (bool)stream.ReceiveNext();
            this.FrisbeeScript.caught = (bool)stream.ReceiveNext();

            this.correctRigidPos = (Vector2)stream.ReceiveNext();
            this.correctRigidVel = (Vector2)stream.ReceiveNext();
        }
    }
}

