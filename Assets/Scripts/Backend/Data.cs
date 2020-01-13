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
            string[] o = row[2].Split(';');
            ObstacleData[] obstacles = new ObstacleData[o.Length / 3];
            for(int i = 0; i + 2 < o.Length; i += 3)
            {
                obstacles[i / 3] = new ObstacleData(o[i], float.Parse(o[i + 1]), float.Parse(o[i + 2]));
            }
            r.Add(new Room(float.Parse(row[0]), int.Parse(row[1]), obstacles));
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
        return (roomDatas[rooms[Random.Range(0, rooms.Count)]]);
    }
}
