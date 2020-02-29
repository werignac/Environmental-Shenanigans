using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTesting : MonoBehaviour
{
    public RoomData room;
    public int player;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Resources.Load("Players/Player" + player), new Vector3(-0.5f * room.width, height), new Quaternion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
