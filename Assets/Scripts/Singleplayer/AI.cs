using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{

    private GameObject frisbee;
    public GameObject AIChar;
    public GameObject player;

    private float moveSpeed = 5;
    private float z;
    public GUITexture powerBar;
    public static float powerValue = 50f;
    public static bool frisbeeCaught = false;

    public Bezier curveThrow;

    public static bool bezierFlight;

    protected float t = 0f;

    private float throwTimer = 0f;
    private float catchTimer = 0f;

    public static bool dashing = false;
    protected float dashTimer = 0f;
    protected bool thrown;

    protected Vector2 desiredPosition;

    protected float rnd;
    private float theta;
    private bool justDashed;
    private float dashCooldown;
    public Frisbee _frisbeeScript;

    void Start()
    {
        frisbee = GameObject.FindWithTag("frisbee");
        AIChar = GameObject.FindWithTag("AI");
        player = GameObject.FindWithTag("Player");

        frisbeeCaught = false;
        bezierFlight = false;

        desiredPosition = new Vector2(16, 0);

    }


    protected void AIPower()
    {
        Rect pos = powerBar.pixelInset;
        pos.xMax = powerBar.pixelInset.xMin + powerValue;
        powerBar.pixelInset = pos;

        powerValue = Mathf.Clamp(powerValue, 0, 100);
    }

    protected void AICatch()
    {
        thrown = false;
        if (!thrown)
        {
            dashing = false;
            dashTimer = 0f;
            frisbee.transform.parent = AIChar.transform;
            frisbee.transform.localPosition = new Vector2(-0.25f, 0.0f);
            frisbee.rigidbody2D.velocity = new Vector2(0, 0);

            frisbee.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            AIChar.transform.LookAt(player.transform);
            AIChar.transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);

            if (player.transform.position.y >= 2.7)
            {
                AIThrow(-5);
            }
            else if (player.transform.position.y <= -2.7f)
            {
                AIThrow(5);
            }
            else
            {
                if (rnd < 1.5f)
                {
                    AIThrow(5);
                }
                else if (rnd >= 1.5f)
                {
                    AIThrow(-5);
                }
            }
            
        }

    }

    protected void BezierMovement()
    {
        if (_frisbeeScript.caught == false)
        {
            if (bezierFlight == true)
            {
                transform.DetachChildren();
                Vector3 vec = curveThrow.GetPointAtTime(t);
                frisbee.transform.position = vec;
                t += powerValue/5000;
                if (t > 1f)
                {
                    bezierFlight = false;
                    frisbee.rigidbody2D.AddForce(new Vector2(0, 0));
                }
            }
        }
        if (bezierFlight == false)
        {
            t = 0f;
        }
    }

    protected void AIThrow(float y)
    {
        
        catchTimer += Time.deltaTime;
        if (catchTimer >= 0.5f)
        {
            if (y > 0)
            {
                curveThrow = new Bezier(frisbee.transform.position, new Vector2(-0f, 0f), new Vector2(20f, 5f), new Vector2(-18f, y));
            }
            else if (y < 0)
            {
                curveThrow = new Bezier(frisbee.transform.position, new Vector2(-0f, 0f), new Vector2(20f, -5f), new Vector2(-18f, y));
            }
            thrown = true;
            bezierFlight = true;
            frisbeeCaught = false;
            AIChar.rigidbody2D.velocity = Vector2.zero;
        }
    }

    protected void AIDash(float distance)
    {

        if (!frisbeeCaught)
        {

            if (distance < 5f && distance > 3f)
            {
                dashing = true;

                if (dashing)
                {
                    dashTimer += Time.deltaTime;
                    //float theta = frisbee.transform.eulerAngles.z * Mathf.Deg2Rad;
                    float x = 2.5f * Mathf.Sin(theta);
                    float y = 2.5f * Mathf.Cos(theta);
                    Vector2 directionVector = new Vector2(x, y);
                    directionVector.Normalize();
                    AIChar.transform.position = Vector2.Lerp(AIChar.transform.position, new Vector2(AIChar.transform.position.x + directionVector.x, AIChar.transform.position.y - directionVector.y), 20 * Time.deltaTime);

                    if (dashTimer > 0.3f)
                    {
                        dashing = false;
                        dashTimer = 0f;
                        justDashed = true;
                    }
                }
            }
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(thrown);
        // if (frisbee.transform.position.x < 4)
        // {
        //     frisbeeCaught = false;
        // }

        rnd = Random.Range(1f, 2f);


        AIPower();
        float distance = Vector2.Distance(AIChar.transform.position, frisbee.transform.position);

        if (frisbeeCaught)
        {
            AICatch();
            dashing = false;
            dashTimer = 0f;

        }
        else if (!frisbeeCaught)
        {
            transform.DetachChildren();

            AIChar.transform.eulerAngles = new Vector3(0, 0, z);
            //AIChar.transform.position += AIChar.transform.up * moveSpeed * Time.deltaTime;

            if (!thrown)
            {
                if (frisbee.transform.position.x > 1)
                {

                    AIChar.transform.position += AIChar.transform.up * moveSpeed * Time.deltaTime;
                    if (!PlayerControls.special)
                    {
                        AIDash(distance);
                    }

                    if (!dashing)
                    {
                        z = Mathf.Atan2((frisbee.transform.position.y - AIChar.transform.position.y), (frisbee.transform.position.x - AIChar.transform.position.x)) * Mathf.Rad2Deg - 90;
                        AIChar.transform.position += AIChar.transform.up * moveSpeed * Time.deltaTime;

                    }
                    else if (dashing)
                    {
                        z = AIChar.transform.eulerAngles.z;
                    }

                }
                else if (frisbee.transform.position.x < -1)
                {
                    transform.position = Vector2.Lerp(transform.position, desiredPosition, moveSpeed * Time.deltaTime);


                }
            }
        }

        if (!dashing)
        {
            z = Mathf.Atan2((frisbee.transform.position.y - AIChar.transform.position.y), (frisbee.transform.position.x - AIChar.transform.position.x)) * Mathf.Rad2Deg - 90;
            theta = frisbee.transform.eulerAngles.z * Mathf.Deg2Rad;

        }

        if (_frisbeeScript.caught)
        {
            bezierFlight = false;
        }

        BezierMovement();

        if (justDashed)
        {
            dashCooldown += Time.deltaTime;
            rigidbody2D.velocity = Vector2.zero;
            if (dashCooldown >= 0.5f)
            {
                justDashed = false;
                dashCooldown = 0f;
            }
        }

        if(thrown)
        {
            throwTimer += Time.deltaTime;
            if(throwTimer >= 1f)
            {
                throwTimer = 0f;
                thrown = false;
            }
        }
    }
}
