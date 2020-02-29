using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    Room[] rooms;
    public Level(int numRooms, int addedRoomNum)
    {
        rooms = new Room[numRooms];
        rooms[0] = Data.GetRoom(addedRoomNum);
        rooms[numRooms - 1] = Data.GetRoom(1 + addedRoomNum);
        int nextRoom = rooms[0].NextRoom;
        for(int i = 1; i < numRooms - 1; ++i)
        {
            rooms[i] = Data.GetRoom(nextRoom);
            nextRoom = rooms[i].NextRoom;
        }
        float pos = 0;
        int healthCount = Random.Range(4, 6);
        for (int i = 0; i < numRooms; ++i)
        {
            rooms[i].SetPos(pos);
            pos += rooms[i].Width / 2;
            if(i < numRooms - 1)
            {
                pos += rooms[i + 1].Width / 2;
            }
            --healthCount;
            if(healthCount <= 0)
            {
                healthCount = Random.Range(2, 5);
                rooms[i].HealthRoom();
            }
            else
            {
                rooms[i].NormalRoom();
            }
        }
    }
    public int RoomNum
    {
        get { return (rooms.Length); }
    }
    public Room GetRoom(int pos)
    {
        return (rooms[pos]);
    }
    public void Destroy()
    {
        for(int i = 0; i < rooms.Length; ++i)
        {
            rooms[i].Destroy();
        }
    }
}
