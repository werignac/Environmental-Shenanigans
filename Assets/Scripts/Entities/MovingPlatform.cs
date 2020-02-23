using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 moveDistance;
    public float speed;
    private Vector2 orgin;
    private Vector2 destination;
    private bool toOrgin;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        orgin = transform.position;
        destination = orgin + moveDistance;
        toOrgin = false;
        angle = Mathf.Atan(moveDistance.x / moveDistance.y);
    }

    // Update is called once per frame
    void Update()
    {
        float s = speed;
        if (toOrgin)
        {
            s *= -1;
        }
        float distance = speed / Data.frameRate;
        transform.Translate(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);
        if (toOrgin)
        {
            if (((destination.x > orgin.x && transform.position.x >= destination.x) || (destination.x < orgin.x && transform.position.x <= destination.x) || destination.x == orgin.x) && 
                ((destination.y > orgin.y && transform.position.y >= destination.y) || (destination.y < orgin.y && transform.position.y <= destination.y) || destination.y == orgin.y))
            {
                toOrgin = true;
            }
        }
        else
        {
            if (((destination.x > orgin.x && transform.position.x <= orgin.x) || (destination.x < orgin.x && transform.position.x >= orgin.x) || destination.x == orgin.x) && 
                ((destination.y > orgin.y && transform.position.y <= orgin.y) || (destination.y < orgin.y && transform.position.y >= orgin.y) || destination.y == orgin.y))
            {
                toOrgin = true;
            }
        }
    }
}
