using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData
{
    float xPos;
    float yPos;
    string name;
    GameObject model;
    public ObstacleData(string n, float x, float y)
    {
        name = n;
        xPos = x;
        yPos = y;
        model = null;
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
    public void Spawn(float roomPos)
    {
        if (model == null)
        {
            model = GameObject.Instantiate(Resources.Load<GameObject>("Obstacles/" + name), new Vector3(roomPos + xPos, yPos, 0), new Quaternion());
        }
    }
    public void DeSpawn()
    {
        if(model != null)
        {
            GameObject.Destroy(model);
            model = null;
        }
    }
}
