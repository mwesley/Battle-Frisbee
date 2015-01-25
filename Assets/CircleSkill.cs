using UnityEngine;
using System.Collections;

public class CircleSkill : PlayerControls
{
    private float _speed;

    // Use this for initialization
    void Start()
    {
        _speed = 1;
    }

    private void CircleSpecial()
    {
        if(special)
        {
        _speed += Time.deltaTime * 10;
                    
            transform.DetachChildren();
            Vector2 frisbeeVelocity = new Vector2(40 * Mathf.Cos(Time.time * 15) + _speed, 40 * Mathf.Sin(Time.time * 15));
            frisbee.rigidbody2D.velocity = frisbeeVelocity;
            Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D);
        }
        if (!special)
        {
            _speed = 1f;
            Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Throw();
        PowerBar();
        BezierMovement();
        Dash();
        CircleSpecial();
    }
}
