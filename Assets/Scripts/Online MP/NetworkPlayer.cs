﻿using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{
    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;
    private string correctTag;


    void Start()
    {

    }

    void Update()
    {
        if(!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 12);
            if (Mathf.Abs(transform.rotation.eulerAngles.z - correctPlayerRot.eulerAngles.z) >= 2f)
            {
                transform.rotation = correctPlayerRot;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 12);
            }
            tag = this.correctTag;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(tag);
        }
        else
        {
            //Network player, receive data
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
            this.correctTag = (string)stream.ReceiveNext();
        }
    }



}
