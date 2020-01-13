using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Level level;
    // Start is called before the first frame update
    void Start()
    {
        Data.LoadRoomDatas();
        level = new Level(4);
        float pos = 0;
        for(int i = 0; i < level.RoomNum; ++i)
        {
            Room room = level.GetRoom(i);
            for(int o = 0; o < room.ObstacleNum; ++o)
            {
                ObstacleData obstacle = room.GetObstacle(o);
                Instantiate(Resources.Load("Obstacles/" + obstacle.Name), new Vector3(obstacle.XPos + pos, obstacle.YPos, 0), new Quaternion());
            }
            pos += room.Width;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
