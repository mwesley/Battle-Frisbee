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
    void Start()
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
        if (col.gameObject.name == "Player")
        {
            frisbeeTransform.parent = playerTransform;
            caught = true;
            frisbeeTransform.localPosition = new Vector2(0.05f, -0.33f);
            rigidbody2D.velocity = new Vector2(0, 0);
        }

        if (col.gameObject.tag == "AI")
        {
            AI.frisbeeCaught = true;
        }

        if (PlayerControls.bezierFlight || AI.bezierFlight || PlayerControls.special)
        {
            if (col.gameObject.tag == "Wall" && !SidleAlongWall.sidle)
            {
                rigidbody2D.velocity = -transform.position.normalized * 15;
            }
            else if (col.gameObject.tag == "Wall" && SidleAlongWall.sidle)
            {
                rigidbody2D.velocity = new Vector2(20, 0);
                PlayerControls.special = false;
            }
        }

        if (col.gameObject.tag == "ScoreWallP1")
        {
            ScoreScript.resetting = true;
            ScoreScript.playerTwoRoundScore++;
            ScoreScript.p2RoundScore.text = "Score: " + ScoreScript.playerTwoRoundScore;
            
        }

        if (col.gameObject.tag == "ScoreWallP2")
        {
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

        PlayerControls.special = false;
        PlayerControls.bezierFlight = false;
        AI.bezierFlight = false;
        AI.dashing = false;
    }



}
