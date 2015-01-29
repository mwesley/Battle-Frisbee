using UnityEngine;
using System.Collections;

public class CursorSelector : MonoBehaviour
{
    private float _x;
    private float _y;
    private Vector2 _direction;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (this.tag == "PlayerOne")
        {
            _x = Input.GetAxis("Horizontal 1");
            _y = Input.GetAxis("Vertical 1");   
        }
        else if (this.tag == "PlayerTwo")
        {
            _x = Input.GetAxis("Horizontal 2");
            _y = Input.GetAxis("Vertical 2");  
        }
        _direction = new Vector2(_x, _y);
        this.transform.Translate(_direction * 0.1f);
    }
}
