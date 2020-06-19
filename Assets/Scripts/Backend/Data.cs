using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// This is where we store data from files, and between scenes.
/// </summary>
public static class Data
{
    private static Room[] roomDatas;
    public static float frameRate;
    private static PlayerData[] playerDatas;
    public static int player;
    public static int rooms;
    public static Vector2 playerPos;
    public static int playerJumps;
    public static int playerDashes;
    public static bool fightingBoss;
    public static bool healthPack;
    public static bool killedEnemy;
    public static float cameraMinX;
    public static float cameraMaxX;


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

    public static void LoadPlayerDatas()
    {
        StreamReader reader = new StreamReader("Data/PlayerData.csv");
        List<PlayerData> p = new List<PlayerData>();
        string line = reader.ReadLine();
        string[] row;
        while ((line = reader.ReadLine()) != null)
        {
            row = line.Split(',');
            p.Add(new PlayerData(row[0], float.Parse(row[1]), float.Parse(row[2]), float.Parse(row[3]), float.Parse(row[4]), int.Parse(row[5]), float.Parse(row[6]), bool.Parse(row[7]), int.Parse(row[8]), float.Parse(row[9]), float.Parse(row[10]), float.Parse(row[11]), bool.Parse(row[12])));
        }
        playerDatas = new PlayerData[p.Count];
        for (int i = 0; i < playerDatas.Length; ++i)
        {
            playerDatas[i] = p[i];
        }
        p = null;
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

    public static PlayerData GetPlayerData(int pos)
    {
        if(playerDatas == null)
        {
            LoadPlayerDatas();
        }
        if(pos < 0 || pos > playerDatas.Length)
        {
            return (null);
        }
        return (playerDatas[pos]);
    }
}
