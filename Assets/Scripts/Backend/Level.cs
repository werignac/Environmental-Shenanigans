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
            rooms[i] = Data.GetRoom(nextRoom);//Spawn rooms based on previous rooms next room type.
            nextRoom = rooms[i].NextRoom;
        }
        float pos = 0;
        int healthCount = Random.Range(3, 5);
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
                healthCount = Random.Range(2, 4);
                rooms[i].HealthRoom();//Set a room to have a health pack.
            }
            else
            {
                rooms[i].NormalRoom();
            }
        }
    }
    public Level(int startingRoom)
    {
        List<Room> r = new List<Room>();
        r.Add(Data.GetRoom(startingRoom));
        while(r[r.Count - 1].NextRoom >= 0)
        {
            Room room = Data.GetRoom(r[r.Count - 1].NextRoom);
            if(room == null)
            {
                break;
            }
            r.Add(room);
        }
        rooms = new Room[r.Count];
        for(int i = 0; i < rooms.Length; ++i)
        {
            rooms[i] = r[i];
        }
        float pos = 0;
        for (int i = 0; i < rooms.Length; ++i)
        {
            rooms[i].SetPos(pos);
            pos += rooms[i].Width / 2;
            if (i < rooms.Length - 1)
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

    //Destroy all room models.
    public void Destroy()
    {
        for(int i = 0; i < rooms.Length; ++i)
        {
            rooms[i].Destroy();
        }
    }

    public void RespawnRoom(int r)
    {
        float pos = rooms[r].GetPos();
        rooms[r].Destroy();
        rooms[r] = Data.GetRoom(rooms[r].RoomType);
        rooms[r].SetPos(pos);
    }
}
