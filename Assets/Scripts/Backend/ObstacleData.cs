using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData
{
    float xPos;
    float yPos;
    string name;
    public ObstacleData(string n, float x, float y)
    {
        name = n;
        xPos = x;
        yPos = y;
    }
    public float XPos
    {
        get { return (xPos); }
    }
    public float YPos
    {
        get { return (yPos); }
    }
    public string Name
    {
        get { return (name); }
    }
}
