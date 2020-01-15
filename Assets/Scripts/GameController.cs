using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Level level;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Data.LoadRoomDatas();
        level = new Level(4);
    }

    // Update is called once per frame
    void Update()
    {
        float pos = 0;
        int r = 0;
        for (int i = 0; i < level.RoomNum; ++i)
        {
            Room room = level.GetRoom(i);
            if(player.transform.position.x >= pos && player.transform.position.x < pos + room.Width)
            {
                r = i;
            }
            pos += room.Width;
        }
        pos = 0;
        for (int i = 0; i < level.RoomNum; ++i)
        {
            Room room = level.GetRoom(i);
            for (int o = 0; o < room.ObstacleNum; ++o)
            {
                if (Mathf.Abs(r - i) < 3)
                {
                    room.GetObstacle(o).Spawn(pos);
                }
                else
                {
                    room.GetObstacle(o).DeSpawn();
                }
            }
            pos += room.Width;
        }
    }
}
