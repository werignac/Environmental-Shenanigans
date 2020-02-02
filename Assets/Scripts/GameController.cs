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
        Data.LoadPlayerDatas();
        level = new Level(5);
    }

    // Update is called once per frame
    void Update()
    {
        float pos = level.GetRoom(0).Width / -2;
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
            if (Mathf.Abs(r - i) < 2)
            {
                room.Spawn();
            }
            else
            {
                room.DeSpawn();
            }
            pos += room.Width;
        }
    }
}
