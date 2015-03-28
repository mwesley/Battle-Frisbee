﻿using UnityEngine;
using System.Collections;

public class MovingObstacle : MonoBehaviour
{
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float speed;
    private bool reversing;
    private bool forwarding;
    private float move = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        this.transform.position = Vector2.Lerp(startPosition, endPosition, move);

        if (move >= 1f)
        {
            reversing = true;
            forwarding = false;
        }
        else if (move <= 0f)
        {
            reversing = false;
            forwarding = true;
        }

        if (forwarding)
        {
            move += Time.deltaTime * speed;
        }
        else if (reversing)
        {
            move -= Time.deltaTime * speed;
        }
    }
}