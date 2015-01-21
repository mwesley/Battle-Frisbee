using UnityEngine;
using System.Collections;

public class Frisbee : MonoBehaviour
{


    public static GameObject frisbee;
    public static GameObject playerCharacter;
    public static GameObject AIChar;
    public static bool caught = false;

    public static bool playerOneWin = false;
    public static bool playerTwoWin = false;

    public GameObject frisbeePrefab;

    private float timer = 0f;

    private Transform frisbeeTransform;
    private Transform playerTransform;

    private Vector2 frisbeeVelocity;

    // Use this for initialization
    void Awake()
    {
        frisbeeVelocity = new Vector2(-5.0f, 0f);
        rigidbody2D.AddForce(frisbeeVelocity);

        ScoreScript.p1RoundScore = GameObject.Find("P1Score").guiText;
        ScoreScript.p2RoundScore = GameObject.Find("P2Score").guiText;

        playerCharacter = GameObject.FindWithTag("Player");
        frisbee = GameObject.FindWithTag("frisbee");
        AIChar = GameObject.FindWithTag("AI");

        playerOneWin = false;
        playerTwoWin = false;
        caught = false;

        frisbeeTransform = frisbee.transform;
        playerTransform = playerCharacter.transform;
    }



    // Update is called once per frame
    void FixedUpdate()
    {

        

        if (ScoreScript.reset == true)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                Debug.Log("Hello");
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

        PlayerControls.sineing = false;
        PlayerControls.bezierFlight = false;
        AI.bezierFlight = false;
        AI.dashing = false;

        if (col.gameObject.name == "Player")
        {
            Debug.Log("Collided!");
            frisbeeTransform.parent = playerTransform;
            caught = true;
            frisbeeTransform.localPosition = new Vector2(0.05f, -0.33f);
            rigidbody2D.velocity = new Vector2(0, 0);

            JohnQ.sineing = false;
            PlayerControls.bezierFlight = false;
            AI.bezierFlight = false;
        }

        if (col.gameObject.tag == "AI")
        {
            Debug.Log("AI caught");
            AI.frisbeeCaught = true;

            PlayerControls.sineing = false;
            PlayerControls.bezierFlight = false;

        }

        if (PlayerControls.bezierFlight)
        {
            if (col.gameObject.tag == "Wall")
            {
                ContactPoint2D contact = col.contacts[0];
                Vector2 pointOfContact = contact.point;
                if (pointOfContact.x < 0)
                {
                    pointOfContact.x = pointOfContact.x * -1;
                }
                Debug.Log("Bezier collision!");
                frisbeeTransform.rigidbody2D.AddForce(pointOfContact);
                PlayerControls.bezierFlight = false;
            }
        }

        if (col.gameObject.tag == "ScoreWallP1")
        {
            AI.bezierFlight = false;
            PlayerControls.bezierFlight = false;
            PlayerControls.sineing = false;
            ScoreScript.resetting = true;
            ScoreScript.playerTwoRoundScore++;
            ScoreScript.p2RoundScore.text = "Score: " + ScoreScript.playerTwoRoundScore;
            Debug.Log("Left");
            
        }

        if (col.gameObject.tag == "ScoreWallP2")
        {
            AI.bezierFlight = false;
            PlayerControls.bezierFlight = false;
            PlayerControls.sineing = false;
            ScoreScript.resetting = true;
            ScoreScript.playerOneRoundScore++;
            ScoreScript.p1RoundScore.text = "Score: " + ScoreScript.playerOneRoundScore;
            Debug.Log("Right");
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

        if (PlayerControls.sineing)
        {
            if (col.gameObject.tag == "Wall")
            {
                ContactPoint2D contact = col.contacts[0];
                Vector2 pointOfContact = contact.point;
                if (pointOfContact.x < 0)
                {
                    pointOfContact.x = pointOfContact.x * -1;
                }
                Debug.Log("Sin collision!");
                frisbeeTransform.rigidbody2D.AddForce(pointOfContact);
                PlayerControls.sineing = false;
            }
        }
    }



}
