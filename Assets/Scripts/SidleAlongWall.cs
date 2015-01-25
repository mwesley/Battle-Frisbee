using UnityEngine;
using System.Collections;

public class SidleAlongWall : PlayerControls
{
    private float _speed;
    private float _time;
    private float _y;
    private bool _upOrDown;
    public static bool sidle;

    // Use this for initialization
    void Start()
    {
        sidle = false;
        _upOrDown = true;
        frisbee = GameObject.FindWithTag("frisbee");
        powerBar = GameObject.FindWithTag("PowerBar");
    }

    private void WallSidle()
    {
        if (special)
        {
            sidle = true;
            transform.DetachChildren();
            Vector2 frisbeeVelocity = new Vector2(15, _y);
            frisbee.rigidbody2D.velocity = frisbeeVelocity;
            Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D);
        }
        if (!special)
        {
            Physics2D.IgnoreCollision(this.collider2D, frisbee.collider2D, false);
            sidle = false;
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
        WallSidle();

        if (special)
        {
            if (_upOrDown)
            {
                Debug.Log(transform.eulerAngles.z);
                if (transform.eulerAngles.z >= 90)
                    _y = 15;
                else
                    _y = -15;
                _upOrDown = false;
            }
        }
        if (!sidle)
            _upOrDown = true;


    }
}
