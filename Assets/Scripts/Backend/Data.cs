using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Data
{
    private static Room[] roomDatas;

    public static void LoadRoomDatas()
    {
        StreamReader reader = new StreamReader("Data/RoomData.csv");
        List<Room> r = new List<Room>();
        string line = reader.ReadLine();
        string[] row;
        while ((line = reader.ReadLine()) != null)
        {
            row = line.Split(',');
            r.Add(new Room(float.Parse(row[1]), int.Parse(row[2]), row[0], int.Parse(row[3])));
        }
        roomDatas = new Room[r.Count];
        for (int i = 0; i < roomDatas.Length; ++i)
        {
            roomDatas[i] = r[i];
        }
        r = null;
        reader.Close();
    }

    //Returns a room of the corrisponding type. 0 is a starting room, 1 is an ending room, 2 is a normal room.
    public static Room GetRoom(int type)
    {
        List<int> rooms = new List<int>();
        for(int i = 0; i < roomDatas.Length; ++i)
        {
            if(roomDatas[i].RoomType == type)
            {
                rooms.Add(i);
            }
        }
        if(rooms.Count <= 0)
        {
            return (null);
        }
        return (new Room(roomDatas[rooms[Random.Range(0, rooms.Count)]]));
    }
}
