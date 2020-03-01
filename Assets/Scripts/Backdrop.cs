using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backdrop : MonoBehaviour
{
    public GameObject[] backdrops;
    public float[] moveSpeed;
    private float[] backdropSizes;
    private GameObject[] backdrops2;
    
    /// <summary>
    /// Sets up the positions of the sprites.
    /// </summary>
    void Start()
    {
        backdropSizes = new float[backdrops.Length];
        backdrops2 = new GameObject[backdrops.Length];
        for(int i = 0; i < backdrops.Length; ++i)
        {
            backdropSizes[i] = backdrops[i].GetComponent<SpriteRenderer>().bounds.size.x;
            //backdrops[i].transform.position = new Vector3(0, backdrops[i].transform.position.y);
            backdrops2[i] = Instantiate(backdrops[i], new Vector3(backdrops[i].transform.position.x + backdropSizes[i], backdrops[i].transform.position.y), new Quaternion());
        }
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
            float velocity = player.GetComponent<Rigidbody2D>().velocity.x / Data.frameRate;
            for (int i = 0; i < backdrops.Length; ++i)//Move backdrops to make parralax.
            {
                backdrops[i].transform.Translate(new Vector3(velocity * moveSpeed[i], 0));
                backdrops2[i].transform.Translate(new Vector3(velocity * moveSpeed[i], 0));
                if(backdrops[i].transform.position.x - pos > backdropSizes[i])
                {
                    backdrops[i].transform.Translate(-2 * backdropSizes[i], 0, 0);
                }
                if(backdrops[i].transform.position.x - pos < -1 * backdropSizes[i])
                {
                    backdrops[i].transform.Translate(2 * backdropSizes[i], 0, 0);
                }
                if (backdrops2[i].transform.position.x - pos > backdropSizes[i])
                {
                    backdrops2[i].transform.Translate(-2 * backdropSizes[i], 0, 0);
                }
                if (backdrops2[i].transform.position.x - pos < -1 * backdropSizes[i])
                {
                    backdrops2[i].transform.Translate(2 * backdropSizes[i], 0, 0);
                }
            }
        }
    }
}