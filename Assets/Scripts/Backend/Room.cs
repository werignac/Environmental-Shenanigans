using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    float width;
    int roomType;
    string name;
    int nextRoomType;
    GameObject model;
    public Room(float w, int r, string n, int next)
    {
        width = w;
        roomType = r;
        name = n;
        nextRoomType = next;
        model = null;
        model = GameObject.Instantiate(Resources.Load<GameObject>("Rooms/" + name), new Vector3(0, 0), new Quaternion());
        model.SetActive(false);
    }
    public Room(Room other)
    {
        width = other.width;
        roomType = other.roomType;
        name = "" + other.name;
        nextRoomType = other.nextRoomType;
        model = GameObject.Instantiate(Resources.Load<GameObject>("Rooms/" + name), new Vector3(0, 0), new Quaternion());
        model.SetActive(false);
    }
    public float Width
    {
        get { return (width); }
    }
    public int RoomType
    {
        get { return (roomType); }
    }
    public int NextRoom
    {
        get { return (nextRoomType); }
    }
    public void SetPos(float pos)
    {
        model.transform.position = new Vector3(pos, 0);
    }
    public float GetPos()
    {
        return (model.transform.position.x);
    }
    public void Spawn()
    {
        model.SetActive(true);
    }
    public void DeSpawn()
    {
        model.SetActive(false);
    }

    public void HealthRoom()//Set model so that healthPacks are available.
    {
        model.GetComponent<RoomData>().HealthRoom();
    }

    public void NormalRoom()//SetModel so that healthpacks aren't available.
    {
        model.GetComponent<RoomData>().NormalRoom();
    }

    public Vector2 GetSpawnPoint()
    {
        return (model.GetComponent<RoomData>().GetSpawnPoint());
    }

    public void Destroy()
    {
        GameObject.Destroy(model);
    }
}
