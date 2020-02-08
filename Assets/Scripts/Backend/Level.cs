﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    Room[] rooms;
    public Level(int numRooms)
    {
        rooms = new Room[numRooms];
        rooms[0] = Data.GetRoom(0);
        rooms[numRooms - 1] = Data.GetRoom(1);
        int nextRoom = rooms[0].NextRoom;
        for(int i = 1; i < numRooms - 1; ++i)
        {
            rooms[i] = Data.GetRoom(nextRoom);
            nextRoom = rooms[i].NextRoom;
        }
        float pos = 0;
        for(int i = 0; i < numRooms; ++i)
        {
            rooms[i].SetPos(pos);
            pos += rooms[i].Width / 2;
            if(i < numRooms - 1)
            {
                pos += rooms[i + 1].Width / 2;
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
}
