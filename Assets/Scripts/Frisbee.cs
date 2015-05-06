using UnityEngine;
using System.Collections;

public class Frisbee : MonoBehaviour
{


    public GameObject frisbee;
    public GameObject playerCharacter;
    public GameObject AIChar;
    public bool caught = false;

    public bool playerOneWin = false;
    public bool playerTwoWin = false;

    public GameObject frisbeePrefab;

    private float timer = 0f;

    private Transform frisbeeTransform;
    private Transform playerTransform;

    private Vector2 frisbeeVelocity;

    private GameObject _wall;

    public Transform dustEffect;
    private GameObject dust;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(DestroyDustLeftovers());

        frisbeeVelocity = new Vector2(-5.0f, 5f);
        rigidbody2D.AddForce(frisbeeVelocity);

        playerCharacter = GameObject.FindWithTag("Player");
        frisbee = GameObject.FindWithTag("frisbee");
        AIChar = GameObject.FindWithTag("AI");

        playerOneWin = false;
        playerTwoWin = false;
        caught = false;

        frisbeeTransform = frisbee.transform;
        playerTransform = playerCharacter.transform;
        _wall = GameObject.FindGameObjectWithTag("Wall");
    }

    IEnumerator DestroyDustLeftovers()
    {
        while (true)
        {
            GameObject[] dustEffects = GameObject.FindGameObjectsWithTag("Dust");      
            foreach (GameObject dustEffect in dustEffects)
            {
                Debug.Log("Test");
                Destroy(dustEffect);
            }
            yield return new WaitForSeconds(5.0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ScoreScript.reset == true)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                frisbeeVelocity = -frisbeeVelocity;
                frisbee.rigidbody2D.AddForce(frisbeeVelocity);
                ScoreScript.reset = false;
                timer = 0f;
            }
        }

        float x = rigidbody2D.velocity.x;
        float y = rigidbody2D.velocity.y;
        float z = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(90.0f - z, Vector3.forward);

        if (playerOneWin)
        {
            ScoreScript.playerOneWin = true;
        }
        if (playerTwoWin)
        {
            ScoreScript.playerTwoWin = true;
        }



    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerControls.special = false;
            PlayerControls.bezierFlight = false;
            AI.bezierFlight = false;
            frisbeeTransform.parent = playerTransform;
            caught = true;
            frisbeeTransform.localPosition = new Vector2(0.0f, 0.0f);
            rigidbody2D.velocity = new Vector2(0, 0);
            PlayerControls.powerValue += 10;
        }

        if (col.gameObject.tag == "AI")
        {
            PlayerControls.special = false;
            PlayerControls.bezierFlight = false;
            AI.bezierFlight = false;
            AI.frisbeeCaught = true;
            AI.powerValue += 10;
        }

        if (PlayerControls.bezierFlight || AI.bezierFlight || PlayerControls.special)
        {
            rigidbody2D.velocity = -transform.position.normalized * 15;
            PlayerControls.special = false;
            if (col.gameObject.tag == "Wall" && SidleAlongWall.sidle)
            {
                rigidbody2D.velocity = new Vector2(20, 0);
                PlayerControls.special = false;
            }
        }

        if (col.gameObject.tag == "ScoreWallP1")
        {
            PlayerControls.special = false;
            PlayerControls.bezierFlight = false;
            AI.bezierFlight = false;
            ScoreScript.resetting = true;
            ScoreScript.playerTwoRoundScore++;
            ScoreScript.p2RoundScore.text = "Score: " + ScoreScript.playerTwoRoundScore;

        }

        if (col.gameObject.tag == "ScoreWallP2")
        {
            PlayerControls.special = false;
            PlayerControls.bezierFlight = false;
            AI.bezierFlight = false;
            ScoreScript.resetting = true;
            ScoreScript.playerOneRoundScore++;
            ScoreScript.p1RoundScore.text = "Score: " + ScoreScript.playerOneRoundScore;
        }

        if (ScoreScript.playerOneRoundScore == 5)
        {
            Debug.Log("Player one wins!");
            ScoreScript.gameEnd = true;
            playerOneWin = true;
        }

        if (ScoreScript.playerTwoRoundScore == 5)
        {
            Debug.Log("Player two wins!");
            ScoreScript.gameEnd = true;
            playerTwoWin = true;
        }

        PlayerControls.bezierFlight = false;
        AI.bezierFlight = false;
        AI.dashing = false;

        foreach (ContactPoint2D contact in col.contacts)
        {
            DustEffect(contact.point);
        }
    }

    void DustEffect(Vector2 contactPoint)
    {
        dust = Instantiate(dustEffect, contactPoint, Quaternion.identity) as GameObject;
    }





}
