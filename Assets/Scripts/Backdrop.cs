using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backdrop : MonoBehaviour
{
    public GameObject background1Sprite1;
    public GameObject background1Sprite2;
    public GameObject background2Sprite1;
    public GameObject background2Sprite2;
    private float b1Width;
    private float b2Width;
    
    /// <summary>
    /// Sets up the positions of the sprites.
    /// </summary>
    void Start()
    {
        b1Width = background1Sprite1.GetComponent<SpriteRenderer>().bounds.size.x;
        b2Width = background2Sprite1.GetComponent<SpriteRenderer>().bounds.size.x;
        if (background1Sprite1 != null)
        {
            background1Sprite1.transform.position = new Vector3(0, background1Sprite1.transform.position.y);
        }
        if (background1Sprite2 != null)
        {
            background1Sprite2.transform.position = new Vector3(b1Width, background1Sprite2.transform.position.y);
        }
        if (background2Sprite1 != null)
        {
            background2Sprite1.transform.position = new Vector3(0, background2Sprite1.transform.position.y);
        }
        if (background2Sprite2 != null)
        {
            background2Sprite2.transform.position = new Vector3(b2Width, background2Sprite2.transform.position.y);
        }
    }

    /// <summary>
    /// Moves the backdrops according to player speed.
    /// </summary>
    void Update()
    {
        PlayerScrollController pSC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScrollController>();
        if(pSC != null)
        {
            float velocity = -1 * pSC.GetVelocity();
            //Move images based on the speed.
            background1Sprite1.transform.Translate(new Vector3(velocity / 100f, 0));
            background1Sprite2.transform.Translate(new Vector3(velocity / 100f, 0));
            background2Sprite1.transform.Translate(new Vector3(velocity / 500f, 0));
            background2Sprite2.transform.Translate(new Vector3(velocity / 500f, 0));
            //Moves images around to have constant scrolling.
            if(background1Sprite1.transform.position.x > b1Width)
            {
                background1Sprite1.transform.position = new Vector3(-1 * b1Width, background1Sprite1.transform.position.y);
            }
            if (background1Sprite1.transform.position.x < -1 * b1Width)
            {
                background1Sprite1.transform.position = new Vector3(b1Width, background1Sprite1.transform.position.y);
            }

            if (background1Sprite2.transform.position.x > b1Width)
            {
                background1Sprite2.transform.position = new Vector3(-1 * b1Width, background1Sprite2.transform.position.y);
            }
            if (background1Sprite2.transform.position.x < -1 * b1Width)
            {
                background1Sprite2.transform.position = new Vector3(b1Width, background1Sprite2.transform.position.y);
            }

            if (background2Sprite1.transform.position.x > b2Width)
            {
                background2Sprite1.transform.position = new Vector3(-1 * b2Width, background2Sprite1.transform.position.y);
            }
            if (background2Sprite1.transform.position.x < -1 * b2Width)
            {
                background2Sprite1.transform.position = new Vector3(b2Width, background2Sprite1.transform.position.y);
            }

            if (background2Sprite2.transform.position.x > b2Width)
            {
                background2Sprite2.transform.position = new Vector3(-1 * b2Width, background2Sprite2.transform.position.y);
            }
            if (background2Sprite2.transform.position.x < -1 * b2Width)
            {
                background2Sprite2.transform.position = new Vector3(b2Width, background2Sprite2.transform.position.y);
            }
        }
    }
}
