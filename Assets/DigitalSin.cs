using UnityEngine;
using System.Collections;

public class DigitalSin : PlayerControls
{

    private float _speed;
    private float _y;
    private float _x;
    private float _time;

    // Use this for initialization
    void Start()
    {
        _speed = 1;
        _time = 0f;
    }

    private void DigitalSinWave()
    {
        _y = ((15 * Mathf.Round(Mathf.Cos((_time * 10)/2))));
        _x = ((powerValue/2.5f * Mathf.Ceil(Mathf.Cos((_time * 10)+3))));

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
            _speed = 1f;
            _time = 0f;
            Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DigitalSinWave();
        Movement();
        Throw();
        PowerBar();
        BezierMovement();
        Dash();
    }
}
