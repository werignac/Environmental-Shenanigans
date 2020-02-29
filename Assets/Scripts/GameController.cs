using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Level level;
    public GameObject player;
    public HealthPoints playerHealth;
    private int l;
    // Start is called before the first frame update
    void Start()
    {
        l = 0;
        Data.LoadRoomDatas();
        Data.LoadPlayerDatas();
        if(player == null)
        {
            player = Instantiate(Resources.Load<GameObject>("Players/Player" + Data.player), new Vector3(0, 1, 0), new Quaternion());

            player.GetComponentInChildren<HitArea>().healthPoints = playerHealth;
        }
        level = new Level(Data.rooms, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float pos = level.GetRoom(0).Width / -2;
        int r = 0;
        for (int i = 0; i < level.RoomNum; ++i)
        {
            Room room = level.GetRoom(i);
            if(player.transform.position.x >= pos && player.transform.position.x < pos + room.Width)
            {
                r = i;
            }
            pos += room.Width;
        }
        pos = 0;
        for (int i = 0; i < level.RoomNum; ++i)
        {
            Room room = level.GetRoom(i);
            if (Mathf.Abs(r - i) < 2)
            {
                room.Spawn();
            }
            else
            {
                room.DeSpawn();
            }
            pos += room.Width;
        }
        if (player.transform.position.y < -50)
        {
            player.transform.position = level.GetRoom(r).GetSpawnPoint();
            player.GetComponent<Rigidbody2D>().velocity = new Vector2();
            player.GetComponentInChildren<HitArea>().Damage();
        }
        if (player.transform.position.x > level.GetRoom(0).Width && r == 0)
        {
            ++l;
            if (l >= 2)
            {
                SceneManager.LoadScene("Win");
            }
            else
            {
                level.Destroy();
                level = new Level(Data.rooms, l * 10);
                player.transform.position = new Vector3(0, 2);
                player.GetComponentInChildren<HitArea>().Heal(10);
            }
        }
    }
}
