using UnityEngine;
using System.Collections;

public class JohnQ : PlayerControls
{

    private float speed;

    // Use this for initialization
    void Start()
    {
        speed = 1;
    }

    private void SinWave()
    {
        if (sineing)
        {
            speed += Time.deltaTime * 10;
            Debug.Log("Sin Wave!");
            transform.DetachChildren();
            Vector2 frisbeeVelocity = new Vector2(40 * Mathf.Cos(Time.time * 15) + speed, 40 * Mathf.Sin(Time.time * 15));
            frisbee.rigidbody2D.velocity = frisbeeVelocity;
        }
        if (!sineing)
        {
            speed = 1f;
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
            SinWave();
        //Debug.Log (powerValue);
    }

}
