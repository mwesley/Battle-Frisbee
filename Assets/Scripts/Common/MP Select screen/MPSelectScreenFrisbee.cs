using UnityEngine;
using System.Collections;

public class MPSelectScreenFrisbee : MonoBehaviour
{
    public string Selected;
    public string Highlighted;
    public int SelectedFloat;

    private float _y;
    private float _x;
    private float _yFactor;
    private float _ySpeed;
    private float _time;
    private Vector2 frisbeeVelocity;
    private float xPos;

    // Use this for initialization
    void Start()
    {
        _time = 0f;
        _ySpeed = 2.5f;
        _x = 10f;
    }
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "FrisbeeP1")
        {
            xPos = -5.07f;
            if (this.transform.position.x >= 0)
            {
                rigidbody2D.MovePosition(new Vector2(-9.5f, transform.position.y));
            }
        }
        else if (this.gameObject.tag == "FrisbeeP2")
        {
            xPos = 5.07f;
            if (this.transform.position.x >= 9.5f)
            {
                rigidbody2D.MovePosition(new Vector2(0f, transform.position.y));
            }
            if (this.transform.position.y >= 5.2f)
            {
                rigidbody2D.MovePosition(new Vector2(transform.position.x, 2.5f));
            }
        }

        if (this.transform.position.y >= 6f || this.transform.position.y <= -2f)
        {
            rigidbody2D.MovePosition(new Vector2(transform.position.x, 2.5f));
        }

        switch(Highlighted)
        {
            case "Sawtooth" :

                _x = 10;
                _time += Time.deltaTime;
                if((_time * _ySpeed) % 2f >= 1)
                {
                    _yFactor = 1;
                }
                else if ((_time * _ySpeed) % 2f < 1)
                {
                    _yFactor = -1;
                }
                _y = _yFactor * 10;
                frisbeeVelocity = new Vector2(_x, _y);
                this.rigidbody2D.velocity = frisbeeVelocity;

                break;

            case "Analog" :

                _y = (20 * Mathf.Cos(_time * 10));
                _x = 10f;
                _time += Time.deltaTime;
                frisbeeVelocity = new Vector2(_x, _y);
                this.rigidbody2D.velocity = frisbeeVelocity;

                break;

            case "Digital" :

                _y = ((5 * Mathf.Round(Mathf.Cos((_time * 10) / 2))));
                _x = (((5) * Mathf.Ceil(Mathf.Cos((_time * 10) + 3))));
                _time += Time.deltaTime;
                frisbeeVelocity = new Vector2(_x, _y);
                this.rigidbody2D.velocity = frisbeeVelocity;

                break;

            case "Slide" :

                this.rigidbody2D.MovePosition(new Vector2(xPos, 2.5f));

                break;

            case "Loop" :

                
                _y = (20 * Mathf.Sin(_time * 10));
                _x = (20 * Mathf.Cos(_time * 10));
                _time += Time.deltaTime;
                frisbeeVelocity = new Vector2(_x, _y);
                this.rigidbody2D.velocity = frisbeeVelocity;

                break;
        }

        switch (Selected)
        {
            case "Sawtooth":

                SelectedFloat = 4;

                break;

            case "Analog":

                SelectedFloat = 1;

                break;

            case "Digital":

                SelectedFloat = 2;

                break;

            case "Slide":

                SelectedFloat = 3;

                break;

            case "Loop":

                SelectedFloat = 5;

                break;
        }
    }
}