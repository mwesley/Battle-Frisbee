using UnityEngine;
using System.Collections;

public class SinWavePlayerOnline : MultiplayerPlayerOnline
{

    private float _y;
    private float _x;
    private float _time;
    private AudioClip specialSound;
    private AudioSource specialSoundSource;

    // Use this for initialization
    void Start()
    {
        _time = 0f;
        frisbee = GameObject.FindWithTag("frisbee");
        powerBar = GameObject.FindWithTag("PowerBar");
        specialSoundSource = (AudioSource)gameObject.AddComponent("AudioSource");
        specialSound = (AudioClip)Resources.Load("sounds/SinWave");
        _FrisbeeScript = frisbee.GetComponent<OnlineFrisbee>();
    }

    private void SinWaveSkill()
    {

        _y = (30 * Mathf.Cos(_time * 10));
        _x = powerValue / 5;

        if (this.tag == "PlayerTwo")
            _x = -_x;

        if (special)
        {
            specialSoundSource.PlayOneShot(specialSound);
            transform.DetachChildren();
            Vector2 frisbeeVelocity = new Vector2(_x, _y);
            frisbee.rigidbody2D.velocity = frisbeeVelocity;
            _time += Time.deltaTime;
            Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D);
        }
        if (!special)
        {
            _time = 0f;
            Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D, false);
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

}
