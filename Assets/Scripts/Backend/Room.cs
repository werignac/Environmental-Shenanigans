using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    float width;
    int roomType;
    ObstacleData[] obstacles;
    public Room(float w, int r, ObstacleData[] o)
    {
        width = w;
        roomType = r;
        obstacles = o;
    }
    public float Width
    {
        get { return (width); }
    }
    public int RoomType
    {
        get { return (roomType); }
    }
    public int ObstacleNum
    {
        get { return (obstacles.Length); }
    }
    public ObstacleData GetObstacle(int pos)
    {
        return (obstacles[pos]);
    }
}
