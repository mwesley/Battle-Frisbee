using UnityEngine;
using System.Collections;

public class RotatingObstacle : MonoBehaviour
{

    private Transform _thisTransform;
    public float Speed;

    // Use this for initialization
    void Start()
    {
        _thisTransform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _thisTransform.Rotate(Vector3.forward * Time.deltaTime * Speed);
    }
}
