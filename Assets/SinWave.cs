using UnityEngine;
using System.Collections;

public class SinWave : PlayerControls
{

    private float speed;
    private float _y;
    private float _time;

    // Use this for initialization
    void Start()
    {
        speed = 1;
        _time = 0f;
    }

    private void SinWaveSkill()
    {
        
        _y = (30 * Mathf.Cos(_time * 10));

        if (special)
        {
            transform.DetachChildren();
            Vector2 frisbeeVelocity = new Vector2(powerValue/5, _y);
            frisbee.rigidbody2D.velocity = frisbeeVelocity;
            _time += Time.deltaTime;
            Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D);
        }
        if (!special)
        {
            speed = 1f;
            _time = 0f;
            Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
            SinWaveSkill();
            Movement();
            Throw();
            PowerBar();
            BezierMovement();
            Dash();

        //Debug.Log (powerValue);
    }

}
