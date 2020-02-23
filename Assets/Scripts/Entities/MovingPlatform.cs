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
        angle = Mathf.Atan(moveDistance.y / moveDistance.x);
        if(moveDistance.x < 0)
        {
            angle += Mathf.PI;
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        Debug.DrawLine(orgin, destination);
#endif
        float s = speed;
        float distance = 0;
        if (Data.frameRate <= 0)
        {
            distance = speed / 30;
        }
        else
        {
            distance = speed / Data.frameRate;
        }
        transform.Translate(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);
        if (!toOrgin)
        {
            if (((destination.x > orgin.x && transform.position.x >= destination.x) || (destination.x < orgin.x && transform.position.x <= destination.x) || destination.x == orgin.x) && 
                ((destination.y > orgin.y && transform.position.y >= destination.y) || (destination.y < orgin.y && transform.position.y <= destination.y) || destination.y == orgin.y))
            {
                toOrgin = true;
                angle += Mathf.PI;
            }
        }
        else
        {
            if (((destination.x > orgin.x && transform.position.x <= orgin.x) || (destination.x < orgin.x && transform.position.x >= orgin.x) || destination.x == orgin.x) && 
                ((destination.y > orgin.y && transform.position.y <= orgin.y) || (destination.y < orgin.y && transform.position.y >= orgin.y) || destination.y == orgin.y))
            {
                toOrgin = false;
                angle -= Mathf.PI;
            }
        }
    }
}
