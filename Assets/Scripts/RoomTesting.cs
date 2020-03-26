using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTesting : MonoBehaviour
{
    public RoomData room;
    public int player;
    public float height;
    public float horizontalOffset;
    private GameObject p;
    // Start is called before the first frame update
    void Start()
    {
        p = Instantiate(Resources.Load<GameObject>("Players/Player" + player), new Vector3(-0.5f * room.width + horizontalOffset, height), new Quaternion());
    }

    // Update is called once per frame
    void Update()
    {
        if (p.transform.position.y < -50)
        {
            p.transform.position = new Vector3(-0.5f * room.width + horizontalOffset, height);
            p.GetComponent<Rigidbody2D>().velocity = new Vector2();
        }
    }
}
