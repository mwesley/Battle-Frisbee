﻿using UnityEngine;
using System.Collections;

public class SawtoothPlayerOne : MultiplayerPlayerOne
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
        _ySpeed = 10f;
        frisbee = GameObject.FindWithTag("frisbee");
        powerBar = GameObject.FindWithTag("PowerBar");
    }

    private void SawtoothSkill()
    {
        if ((_time) % 2f >= 1)
        {
            _yFactor = 1;
        }
        else if ((_time) % 2f < 1)
        {
            _yFactor = -1;
        }
        _y = (_yFactor * _ySpeed);
        _x = (powerValue / 5f);

        if (this.tag == "PlayerTwo")
            _x = -_x;

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
        Throw();
        PowerBar();
        BezierMovement();
        Dash();

        Movement();
    }
}
