using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTesting : MonoBehaviour
{
    public RoomData room;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Resources.Load("Ground"), new Vector3(), new Quaternion());
        Instantiate(Resources.Load("Players/Player0"), new Vector3(-0.5f * room.width, 2), new Quaternion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
