using UnityEngine;
using System.Collections;

public class Sawtooth : PlayerControls
{
    private float _y;
    private float _x;
    private float _time;
    private float _yFactor;
    private float _ySpeed;

    // Use this for initialization
    void Start()
    {
        _time = 0f;
        _ySpeed = 2.5f;
        frisbee = GameObject.FindWithTag("frisbee");
        powerBar = GameObject.FindWithTag("PowerBar");
    }

    private void SawtoothSkill()
    {
        if ((_time * _ySpeed) % 2f >= 1)
        {
            _yFactor = 1;
        }
        else if ((_time * _ySpeed) % 2f < 1)
        {
            _yFactor = -1;
        }
        _y = (_yFactor * 10);
        _x = (powerValue / 5f);
        if (special)
        {
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
        SawtoothSkill();
        Movement();
        Throw();
        PowerBar();
        BezierMovement();
        Dash();
    }
}
