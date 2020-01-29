using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    int frameCounter;
    float timeCounter;
    float refreshTime;

    void Start()
    {
        Data.frameRate = 30;
        refreshTime = 0.05f;
        timeCounter = 0f;
        frameCounter = 0;
    }

    void Update()
    {
        if (timeCounter < refreshTime)
        {
            timeCounter += Time.deltaTime;
            frameCounter++;
        }
        else
        {
            Data.frameRate = (float)frameCounter / timeCounter;
            frameCounter = 0;
            timeCounter = 0.0f;
        }
    }
}
