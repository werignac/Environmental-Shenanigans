using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Level level;
    public GameObject player;
    public HealthPoints playerHealth;
    public GameObject[] backdrops;
    // Start is called before the first frame update
    void Start()
    {
        Data.LoadRoomDatas();
        Data.LoadPlayerDatas();
        if(player == null)
        {
            player = Instantiate(Resources.Load<GameObject>("Players/Player" + Data.player), new Vector3(0, 1, 0), new Quaternion());//Instantiate the player if it's not already in the scene.

            player.GetComponentInChildren<HitArea>().healthPoints = playerHealth;
        }
        level = new Level(Data.rooms, Data.startRoom);
        //Only set the first backdrop active.
        backdrops[Data.level].SetActive(true);
        for(int i = 1; i < backdrops.Length; ++i)
        {
            backdrops[i].SetActive(false);
        }
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
                r = i;//Find which room the player is in.
                Data.cameraMinX = pos + 20;
                Data.cameraMaxX = pos + room.Width - 20;
            }
            pos += room.Width;
        }
        pos = 0;
        //Activate the rooms near the player to save processing by not firing all projectile shooters everywhere.
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
        if(r == level.RoomNum - 1)
        {
            Data.fightingBoss = true;//Set fighting boss such that boss music starts playing.
        }
        if (Data.healthPack)
        {
            GameObject hP = Instantiate(Resources.Load<GameObject>("Obstacles/HealthPack"), level.GetRoom(level.RoomNum - 1).GetSpawnPoint(), new Quaternion());
            hP.GetComponent<HealthPack>().heal = 2;
            Data.healthPack = false;
        }
        if (Data.died || player.transform.position.y < -50)
        {
            //Respawn the player at the start of the room when the player falls off the bottom of the map.
            player.transform.position = level.GetRoom(r).GetSpawnPoint();
            player.GetComponent<Rigidbody2D>().velocity = new Vector2();
            player.GetComponentInChildren<HitArea>().SetHealth(1);
            Data.currentDeaths++;
            Data.died = false;
        }
        if (player.transform.position.x > level.GetRoom(0).Width && r == 0)
        {
            Data.SetDeaths();
            SceneManager.LoadScene("Win");
        }
    }
}
