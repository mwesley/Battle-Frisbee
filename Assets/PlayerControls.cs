using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    // up and down keys (to be set in the Inspector)
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode dash;
    public KeyCode throwFrisbee;
    public KeyCode charge;

    public GameObject frisbee;

    protected bool aimLock = false;
    protected bool charging;
    protected bool dashing;
    public static bool sineing;

    public bool isThrown = false;

    public static bool bezierFlight;

    public static float t = 0f;

    protected float chargeSpeed = 25;
    protected float powerValue = 100;
    protected float timer = 0;
    protected float z;

    public GUITexture powerBar;

    public Bezier curveThrow;

    protected RaycastHit2D hit;
    public Vector2 hitVec;

    public float maxMovementSpeed;

    protected Vector2 directionAim;
    protected Quaternion rotationAim;

    public float dashTime;
    public float dashDistance;
    public float dashSpeed;
    protected bool justDashed;

    protected LayerMask playerMask;
    protected LayerMask centerMask;
    protected int playerMaskValue;
    protected int centerMaskValue;
    protected Vector2 pos;

    protected Vector2 playerDirection;

    protected KeyCombo upCurve = new KeyCombo(new string[] { "down", "right", "Fire1" });
    protected KeyCombo downCurve = new KeyCombo(new string[] { "up", "right", "Fire1" });

    protected KeyCombo sinWave = new KeyCombo(new string[] { "Fire2", "Fire2" });



    void Awake()
    {
        t = 0f;
        playerMaskValue = LayerMask.GetMask("Player");
        centerMaskValue = LayerMask.GetMask("CenterWall");
        Debug.Log(playerMaskValue);
        justDashed = false;
    }

    protected void BezierMovement()
    {
        if (Frisbee.caught == false)
        {
            if (bezierFlight == true)
            {
                Vector3 vec = curveThrow.GetPointAtTime(t);
                frisbee.transform.position = vec;
                t += powerValue / 5000;
                if (t > 1f)
                {
                    bezierFlight = false;
                    frisbee.rigidbody2D.AddForce(hitVec);
                }

            }
        }
        if (bezierFlight == false)
            t = 0f;
    }


    protected void PowerBar()
    {

        if (Input.GetKey(charge))
        {
            Rect pos = powerBar.pixelInset;
            pos.xMax = powerBar.pixelInset.xMin + powerValue;
            powerBar.pixelInset = pos;

            powerValue += Time.deltaTime * chargeSpeed;
            Debug.Log (powerValue);
            powerValue = Mathf.Clamp(powerValue, 0, 100);
            //Debug.Log(powerValue);
            charging = true;



        }
    }

    protected void Throw()
    {

        if (Frisbee.caught == true)
        {

            hit = Physics2D.Raycast(frisbee.transform.position, playerDirection);
            hitVec = hit.point;
            rigidbody2D.velocity = Vector2.zero;
            bezierFlight = false;

            if (upCurve.Check())
            {
                Debug.Log("Upping the curve!");

                bezierFlight = true;
                curveThrow = new Bezier(frisbee.transform.position, new Vector2(10, 7), new Vector2(-10, 7), hitVec);
                transform.DetachChildren();
                Frisbee.caught = false;
            }
            else if (downCurve.Check())
            {
                Debug.Log("Downing the curve!");

                bezierFlight = true;
                curveThrow = new Bezier(frisbee.transform.position, new Vector2(10, -7), new Vector2(-10, -7), hitVec);
                transform.DetachChildren();
                Frisbee.caught = false;
            }
            else if (sinWave.Check())
            {
                sineing = true;
                Frisbee.caught = false;
            } 
            else if (Input.GetButtonDown("Fire1"))
            {
                transform.DetachChildren();
                frisbee.rigidbody2D.AddForce(-transform.up * 10);
                Frisbee.caught = false;
            }
            isThrown = true;

        }

        if (isThrown == true)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            if (timer > 1)
            {
                isThrown = false;
                gameObject.collider2D.enabled = true;
                timer = 0;
            }
        }
    }
    


    protected void Movement()
    {

        playerDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!dashing && !justDashed)
        {
            if (Frisbee.caught == false)
            {

                rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * maxMovementSpeed, Input.GetAxis("Vertical") * maxMovementSpeed);
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");
                z = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(90.0f - z, Vector3.forward);


            }
        }
        if (Frisbee.caught)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            z = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(90.0f - z, Vector3.forward);
        }
    }

    protected void Dash()
    {
        if (!Frisbee.caught)
        {
            if (Input.GetKeyDown(dash) && !justDashed)
            {
                
                Debug.Log("Gotta go fast");
                dashing = true;
            }
        }

        if (dashing)
        {

            float theta = transform.eulerAngles.z * Mathf.Deg2Rad;
            float x = dashDistance * Mathf.Sin(theta);
            float y = dashDistance * Mathf.Cos(theta);
            Vector3 directionVector = new Vector3(x, y, 0);
            directionVector.Normalize();
            timer += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + directionVector.x, transform.position.y - directionVector.y), dashSpeed * Time.deltaTime);
            Debug.Log(directionVector);
            if (timer >= dashTime)
            {
                timer = 0;
                dashing = false;
                justDashed = true;
            }
        }
        if (justDashed)
        {
            timer += Time.deltaTime;
            rigidbody2D.velocity = Vector2.zero;
            if (timer >= 0.5f)
            {
                justDashed = false;
                timer = 0f;
            }
        }
    }



    void FixedUpdate()
    {
        //Time.timeScale = 0.5f;

        pos = new Vector2(transform.position.x, transform.position.y);

        /*if (sineing)
        {
            Debug.Log("Sin Wave!");
            transform.DetachChildren();
            Vector2 frisbeeVelocity = new Vector2(15f, 50 * Mathf.Sin(Time.time * 10));
            frisbee.rigidbody2D.velocity = frisbeeVelocity;
        }*/

        
    }
}
