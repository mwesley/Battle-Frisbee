using UnityEngine;

public class MyBezier : MonoBehaviour
{
    public Bezier myBezier;
    private float t = 0f;

    void Start()
    {
        //myBezier = new Bezier( new Vector3( -5f, 0f, 0f ), new Vector3( -5f, 5f, 0f), new Vector3( -5f, 0f, 0f), new Vector3( 5f, 0f, 0f ) );
    }

    void Update()
    {
        Vector3 vec = myBezier.GetPointAtTime(t);
        transform.position = vec;

        t += 0.01f;
        if (t > 1f)
            t = 0f;
    }
}