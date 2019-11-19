using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backdrop : MonoBehaviour
{
    public GameObject background1Sprite1;
    private GameObject background1Sprite2;
    public GameObject background2Sprite1;
    private GameObject background2Sprite2;
    public GameObject ground1;
    private GameObject ground2;
    private float b1Width;
    private float b2Width;
    private float gWidth;
    
    /// <summary>
    /// Sets up the positions of the sprites.
    /// </summary>
    void Start()
    {
        b1Width = background1Sprite1.GetComponent<SpriteRenderer>().bounds.size.x;
        b2Width = background2Sprite1.GetComponent<SpriteRenderer>().bounds.size.x;
        gWidth = ground1.GetComponent<SpriteRenderer>().bounds.size.x;
        if (background1Sprite1 != null)
        {
            background1Sprite1.transform.position = new Vector3(0, background1Sprite1.transform.position.y);
            background1Sprite2 = Instantiate(background1Sprite1, new Vector3(b1Width, background1Sprite1.transform.position.y), new Quaternion());
        }
        /*if (background1Sprite2 != null)
        {
            background1Sprite2.transform.position = new Vector3(b1Width, background1Sprite2.transform.position.y);
        }*/
        if (background2Sprite1 != null)
        {
            background2Sprite1.transform.position = new Vector3(0, background2Sprite1.transform.position.y);
            background2Sprite2 = Instantiate(background2Sprite1, new Vector3(b2Width, background2Sprite1.transform.position.y), new Quaternion());
        }
        /*if (background2Sprite2 != null)
        {
            background2Sprite2.transform.position = new Vector3(b2Width, background2Sprite2.transform.position.y);
        }*/
        if (ground1 != null)
        {
            ground1.transform.position = new Vector3(0, ground1.transform.position.y);
            ground2 = Instantiate(ground1, new Vector3(gWidth, ground1.transform.position.y), new Quaternion());
        }
        /*if (ground2 != null)
        {
            ground2.transform.position = new Vector3(gWidth, ground2.transform.position.y);
        }*/
    }

    /// <summary>
    /// Moves the backdrops according to player speed.
    /// </summary>
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            float pos = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
            float velocity = player.GetComponent<Rigidbody2D>().velocity.x;
            //Move images based on the speed.
            background1Sprite1.transform.Translate(new Vector3(velocity * 0.0032f, 0));
            background1Sprite2.transform.Translate(new Vector3(velocity * 0.0032f, 0));
            background2Sprite1.transform.Translate(new Vector3(velocity * 0.000998f, 0));
            background2Sprite2.transform.Translate(new Vector3(velocity * 0.000998f, 0));
            //Moves images around to have constant scrolling.
            if(background1Sprite1.transform.position.x - pos > b1Width)
            {
                background1Sprite1.transform.Translate(-2 * b1Width, 0, 0);
            }
            if (background1Sprite1.transform.position.x - pos < -1 * b1Width)
            {
                background1Sprite1.transform.Translate(2 * b1Width, 0, 0);
            }

            if (background1Sprite2.transform.position.x - pos > b1Width)
            {
                background1Sprite2.transform.Translate(-2 * b1Width, 0, 0);
            }
            if (background1Sprite2.transform.position.x - pos < -1 * b1Width)
            {
                background1Sprite2.transform.Translate(2 * b1Width, 0, 0);
            }

            if (background2Sprite1.transform.position.x - pos > b2Width)
            {
                background2Sprite1.transform.Translate(-2 * b2Width, 0, 0);
            }
            if (background2Sprite1.transform.position.x - pos < -1 * b2Width)
            {
                background2Sprite1.transform.Translate(2 * b2Width, 0, 0);
            }

            if (background2Sprite2.transform.position.x - pos> b2Width)
            {
                background2Sprite2.transform.Translate(-2 * b2Width, 0, 0);
            }
            if (background2Sprite2.transform.position.x - pos< -1 * b2Width)
            {
                background2Sprite2.transform.Translate(2 * b2Width, 0, 0);
            }

            if (ground1.transform.position.x - pos> gWidth)
            {
                ground1.transform.Translate(-2 * gWidth, 0, 0);
            }
            if (ground1.transform.position.x - pos< -1 * gWidth)
            {
                ground1.transform.Translate(2 * gWidth, 0, 0);
            }

            if (ground2.transform.position.x - pos > gWidth)
            {
                ground2.transform.Translate(-2 * gWidth, 0, 0);
            }
            if (ground2.transform.position.x - pos< -1 * gWidth)
            {
                ground2.transform.Translate(2 * gWidth, 0, 0);
            }
        }
    }
}
