using UnityEngine;
using System.Collections;

public class SinWavePlayerOnlineTwo : MultiplayerPlayerOnlineTwo
{

    private float _y;
    private float _x;
    private float _time;
    private AudioClip specialSound;
    private AudioSource specialSoundSource;


    // Use this for initialization
    void Awake()
    {
        _time = 0f;
        //frisbee = GameObject.FindWithTag("frisbee");
        powerBar = GameObject.FindWithTag("PowerBar");
        specialSoundSource = (AudioSource)gameObject.AddComponent("AudioSource");
        specialSound = (AudioClip)Resources.Load("sounds/SinWave");
        //_FrisbeeScript = frisbee.GetComponent<OnlineFrisbee>();
    }

    private void SinWaveSkill()
    {

        if (special)
        {
            this.thisPhotonView.RPC("SpecialInProgressTwo", PhotonTargets.All);
        }
        if (!special)
        {
            this.thisPhotonView.RPC("SpecialDone", PhotonTargets.All);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (photonView.isMine && frisbee)
        {
            SinWaveSkill();
            Throw();
            //PowerBar();
            BezierMovement();
            Dash();
            Movement();
        }

    }
    [RPC]
    void SpecialInProgressTwo()
    {
        _y = (30 * Mathf.Cos(_time * 10));
        _x = 10f;

        if (this.tag == "PlayerTwo")
            _x = -_x;

        specialSoundSource.PlayOneShot(specialSound);
        transform.DetachChildren();
        Vector2 frisbeeVelocity = new Vector2(_x, _y);
        frisbee.rigidbody2D.velocity = frisbeeVelocity;
        _time += Time.deltaTime;
        Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D);
    }

    [RPC]
    void SpecialDone()
    {
        _time = 0f;
        Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D, false);
    }
}
