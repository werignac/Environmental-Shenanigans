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
    public static int startRoom;
    public static int level;
    public static Score[][][] levelScores;
    public static int currentDeaths;
    public static bool died;
    public static float time;


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

    public static void LoadScoreData()
    {
        StreamReader reader = new StreamReader("Data/ScoreData.csv");
        levelScores = new Score[3][][];
        string line = reader.ReadLine();
        string[] row;
        for (int i = 0; i < levelScores.Length; ++i)
        {
            levelScores[i] = new Score[3][];
            for(int j = 0; j < levelScores[i].Length; ++j)
            {
                line = reader.ReadLine();
                if(line == null)
                {
                    reader.Close();
                    return;
                }
                row = line.Split(',');
                levelScores[i][j] = new Score[2];
                if(row.Length <= 1)
                {
                    levelScores[i][j][0] = null;
                    levelScores[i][j][1] = null;
                }
                else
                {
                    levelScores[i][j][0] = new Score(int.Parse(row[0]), float.Parse(row[1]));
                    levelScores[i][j][1] = new Score(int.Parse(row[2]), float.Parse(row[3]));
                }
            }
        }
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

    public static void SetScore()
    {
        bool write = false;
        if(levelScores[player - 1][level][0] == null || levelScores[player - 1][level][0].deaths > currentDeaths)
        {
            levelScores[player - 1][level][0] = new Score(currentDeaths, time);
            write = true;
        }
        if(levelScores[player - 1][level][1] == null || levelScores[player - 1][level][1].time > time)
        {
            levelScores[player - 1][level][1] = new Score(currentDeaths, time);
            write = true;
        }
        if (write)
        {
            StreamWriter writer = new StreamWriter("Data/ScoreData.csv");
            writer.WriteLine("LDDeaths,LDTime,LTDeaths,LTTime");
            for(int i = 0; i < levelScores.Length; ++i)
            {
                for(int j = 0; j < levelScores[i].Length; ++j)
                {
                    if (levelScores[i][j][0] == null)
                    {
                        writer.Write("-");
                    }
                    else
                    {
                        for (int k = 0; k < levelScores[i][j].Length; ++k)
                        {
                            if (k != 0)
                            {
                                writer.Write(",");
                            }
                            writer.Write(levelScores[i][j][k].deaths + "," + levelScores[i][j][k].time);
                        }
                    }
                    writer.Write("\n");
                }
            }
            writer.Close();
        }
        currentDeaths = 0;
        time = 0;
    }
}
