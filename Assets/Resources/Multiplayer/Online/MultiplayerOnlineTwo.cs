using UnityEngine;
using System.Collections;

public class MultiplayerPlayerOnlineTwo : Photon.MonoBehaviour
{
    // up and down keys (to be set in the Inspector)

    public GameObject frisbee;

    protected bool aimLock = false;
    protected bool charging;
    protected bool dashing;
    public bool special;
    public PhotonView thisPhotonView;

    public bool isThrown = false;

    public bool bezierFlight;

    public float t = 0f;

    protected float chargeSpeed = 25;
    public float powerValue = 50f;
    protected float dashTimer = 0;
    protected float throwTimer = 0;
    protected float dashCooldown = 0;
    protected float z;

    public GameObject powerBar;

    public Bezier curveThrow;

    protected RaycastHit2D hit;
    public Vector2 hitVecOne;

    public float maxMovementSpeed;

    protected Vector2 directionAim;
    protected Quaternion rotationAim;

    public float dashTime;
    public float dashDistance;
    public float dashSpeed;
    protected bool justDashed;

    protected LayerMask playerMask;
    protected LayerMask centerMask;
    protected LayerMask wallMask;
    protected int playerMaskValue;
    protected int centerMaskValue;
    protected int wallMaskValue;
    protected Vector2 pos;

    protected Vector2 playerDirection;

    public OnlineFrisbee _FrisbeeScript;

    protected KeyCombo upCurve = new KeyCombo(new string[] { "down", "left", "Throw" });
    protected KeyCombo downCurve = new KeyCombo(new string[] { "up", "left", "Throw" });
    protected KeyCombo specialAbility = new KeyCombo(new string[] { "Special", "Special" });

    void Start()
    {
        t = 0f;
        playerMaskValue = LayerMask.GetMask("Player");
        centerMaskValue = LayerMask.GetMask("CenterWall");
        wallMaskValue = LayerMask.GetMask("Wall");
        justDashed = false;
        powerValue = 50f;
        thisPhotonView = this.gameObject.GetComponent<PhotonView>();

    }

    protected void BezierMovement()
    {
        if (_FrisbeeScript.caught == false)
        {
            if (bezierFlight == true)
            {
                Vector3 vec = curveThrow.GetPointAtTime(t);
                frisbee.transform.position = vec;
                t += powerValue / 5000;
                if (t > 1f)
                {
                    bezierFlight = false;
                    frisbee.rigidbody2D.AddForce(hitVecOne);
                }

            }
        }
        if (bezierFlight == false)
            t = 0f;
    }


    protected void PowerBar()
    {
        Rect pos = powerBar.guiTexture.pixelInset;
        pos.xMax = powerBar.guiTexture.pixelInset.xMin + powerValue;
        powerBar.guiTexture.pixelInset = pos;

        powerValue = Mathf.Clamp(powerValue, 0, 100);
        // powerValue -= 0.01f;

    }

    
    public void Throw()
    {
        
        if (_FrisbeeScript.PlayerTwoCaught)
        {

            hit = Physics2D.Raycast(frisbee.transform.position, playerDirection, Mathf.Infinity, wallMaskValue);
            hitVecOne = hit.point;
            this.rigidbody2D.velocity = Vector2.zero;
            bezierFlight = false;

            if (upCurve.Check())
            {
                this.thisPhotonView.RPC("UpCurve", PhotonTargets.All);
            }
            else if (downCurve.Check())
            {
                this.thisPhotonView.RPC("DownCurve", PhotonTargets.All);
            }
            else if (specialAbility.Check())
            {
                Debug.Log("Special throw by playertwo");
                this.thisPhotonView.RPC("SpecialThrow", PhotonTargets.All);
            }
            else if (Input.GetButtonDown("Throw"))
            {
                this.thisPhotonView.RPC("ThrowStraight", PhotonTargets.All);
            }


        }

        if (isThrown == true)
        {
            throwTimer += Time.deltaTime;
            if (throwTimer > 1)
            {
                isThrown = false;
                gameObject.collider2D.enabled = true;
                throwTimer = 0;
            }
        }
    }

    protected void Movement()
    {

        playerDirection = new Vector2(Input.GetAxis("Horizontal 1"), Input.GetAxis("Vertical 1"));
        if (!dashing && !justDashed)
        {
            if (!_FrisbeeScript.PlayerTwoCaught)
            {
                rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal 1") * maxMovementSpeed, Input.GetAxis("Vertical 1") * maxMovementSpeed);
                float x = Input.GetAxis("Horizontal 1");
                float y = Input.GetAxis("Vertical 1");
                z = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
                if (z == 0)
                {
                    z = 180;
                }
                transform.rotation = Quaternion.AngleAxis(90.0f - z, Vector3.forward);


            }
        }
        if (_FrisbeeScript.PlayerTwoCaught)
        {
            frisbee.transform.SetParent(this.transform);
            this.rigidbody2D.velocity = Vector2.zero;
            float x = Input.GetAxis("Horizontal 1");
            float y = Input.GetAxis("Vertical 1");
            z = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
            if (z == 0)
            {
                z = 180;
            }
            transform.rotation = Quaternion.AngleAxis(90.0f - z, Vector3.forward);
        }
    }


    protected void Dash()
    {



        if (!_FrisbeeScript.caught)
        {
            if (Input.GetButtonDown("Dash") && !justDashed)
            {
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
            dashTimer += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + directionVector.x, transform.position.y - directionVector.y), dashSpeed * Time.deltaTime);
            if (dashTimer >= dashTime || _FrisbeeScript.caught)
            {
                dashTimer = 0;
                dashing = false;
                justDashed = true;
            }
        }
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
    }

    void FixedUpdate()
    {
        pos = new Vector2(transform.position.x, transform.position.y);
    }

    [RPC] protected void ThrowStraight()
    {
        transform.DetachChildren();
        frisbee.rigidbody2D.AddForce(-transform.up * powerValue / 6.66f);
        _FrisbeeScript.PlayerTwoCaught = false;
        isThrown = true;
        _FrisbeeScript.caught = false;
        Debug.Log("Player two throw");
    }

    [RPC] protected void UpCurve()
    {
        Debug.Log("Upping the curve!");

        bezierFlight = true;
        curveThrow = new Bezier(frisbee.transform.position, new Vector2(-10, 7), new Vector2(10, 7), hitVecOne);
        transform.DetachChildren();
        _FrisbeeScript.PlayerTwoCaught = false;
        isThrown = true;
        _FrisbeeScript.caught = false;
    }

    [RPC] protected void DownCurve()
    {
        Debug.Log("Downing the curve!");

        bezierFlight = true;
        curveThrow = new Bezier(frisbee.transform.position, new Vector2(-10, -7), new Vector2(10, -7), hitVecOne);
        transform.DetachChildren();
        _FrisbeeScript.PlayerTwoCaught = false;
        isThrown = true;
        _FrisbeeScript.caught = false;
    }

    [RPC] protected void SpecialThrow()
    {
        special = true;
        transform.DetachChildren();
        _FrisbeeScript.PlayerTwoCaught = false;
        isThrown = true;
        _FrisbeeScript.caught = false;
    }
}
