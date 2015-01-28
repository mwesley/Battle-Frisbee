using UnityEngine;
using System.Collections;

public class MPFrisbee : MonoBehaviour
{


    private GameObject _frisbee;
    private GameObject _PlayerOne;
    private GameObject _PlayerTwo;
    public bool caught = false;
    public bool PlayerOneCaught = false;
    public bool PlayerTwoCaught = false;

    public bool playerOneWin = false;
    public bool playerTwoWin = false;

    public GameObject MultiplayerFrisbee;

    private float timer = 0f;

    private Vector2 frisbeeVelocity;

    private GameObject _wall;

    // Use this for initialization
    void Start()
    {
        frisbeeVelocity = new Vector2(-5.0f, 0f);
        rigidbody2D.AddForce(frisbeeVelocity);

        _PlayerOne = GameObject.FindWithTag("PlayerOne");
        _PlayerTwo = GameObject.FindWithTag("PlayerTwo");
        _frisbee = GameObject.FindWithTag("frisbee");

        playerOneWin = false;
        playerTwoWin = false;
        PlayerOneCaught = false;
        PlayerTwoCaught = false;
        caught = false;

        _wall = GameObject.FindGameObjectWithTag("Wall");
    }



    // Update is called once per frame
    void FixedUpdate()
    {

        float x = rigidbody2D.velocity.x;
        float y = rigidbody2D.velocity.y;
        float z = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(90.0f - z, Vector3.forward);


        if(!_PlayerOne || !_PlayerTwo || !_frisbee)
        {
        _PlayerOne = GameObject.FindWithTag("PlayerOne");
        _PlayerTwo = GameObject.FindWithTag("PlayerTwo");
        _frisbee = GameObject.FindWithTag("frisbee");
        }

        if(PlayerOneCaught || PlayerTwoCaught)
        {
            caught = true;
        }
        else if (!PlayerOneCaught || !PlayerTwoCaught)
        {
            caught = false;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerOne")
        {
            _frisbee.transform.SetParent(_PlayerOne.transform);
            PlayerOneCaught = true;
            _frisbee.transform.localPosition = new Vector2(0.0f, 0.0f);
            rigidbody2D.velocity = new Vector2(0, 0);
        }

        if (col.gameObject.tag == "PlayerTwo")
        {
            _frisbee.transform.SetParent(_PlayerTwo.transform);
            PlayerTwoCaught = true;
            _frisbee.transform.localPosition = new Vector2(0.0f, 0.0f);
            rigidbody2D.velocity = new Vector2(0, 0);
        }

    }



}
