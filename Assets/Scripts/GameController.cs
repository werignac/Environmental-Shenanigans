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
    public GameObject[] backdrops;
    // Start is called before the first frame update
    void Start()
    {
        l = 0;
        Data.LoadRoomDatas();
        Data.LoadPlayerDatas();
        if(player == null)
        {
            player = Instantiate(Resources.Load<GameObject>("Players/Player" + Data.player), new Vector3(0, 1, 0), new Quaternion());//Instantiate the player if it's not already in the scene.

            player.GetComponentInChildren<HitArea>().healthPoints = playerHealth;
        }
        level = new Level(Data.rooms, 0);
        //Only set the first backdrop active.
        backdrops[0].SetActive(true);
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
        if (player.transform.position.y < -50)
        {
            //Respawn the player at the start of the room when the player falls off the bottom of the map.
            player.transform.position = level.GetRoom(r).GetSpawnPoint();
            player.GetComponent<Rigidbody2D>().velocity = new Vector2();
            player.GetComponentInChildren<HitArea>().Damage();
        }
        if (player.transform.position.x > level.GetRoom(0).Width && r == 0)
        {
            backdrops[l].SetActive(false);//Deactivate old backdrop.
            ++l;
            if (l >= 2)
            {
                SceneManager.LoadScene("Win");
            }
            else
            {
                //Reset scene for next level.
                backdrops[l].SetActive(true);
                Data.fightingBoss = false;
                level.Destroy();
                level = new Level(Data.rooms, l * 10);
                player.transform.position = new Vector3(0, 2);
                player.GetComponentInChildren<HitArea>().Heal(10);
            }
        }
    }
}
