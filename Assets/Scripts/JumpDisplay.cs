using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDisplay : MonoBehaviour
{
    private int numJumps;
    private GameObject[] jumpDisplays;
    // Start is called before the first frame update
    void Start()
    {
        jumpDisplays = new GameObject[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            jumpDisplays[i] = child.gameObject;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(numJumps != Data.playerJumps)
        {
            numJumps = Data.playerJumps;
            for(int i = 0; i < Mathf.Min(numJumps, jumpDisplays.Length); ++i)
            {
                jumpDisplays[i].SetActive(true);
            }
            for(int i = numJumps; i < jumpDisplays.Length; ++i)
            {
                jumpDisplays[i].SetActive(false);
            }
        }
    }
}
