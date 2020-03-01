using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AudioSource bGM;
    private GameObject player;
    private bool playingBossMusic;
    // Start is called before the first frame update
    void Start()
    {
        playingBossMusic = false;
        bGM.clip = Resources.Load<AudioClip>("Sounds/TestMainMenu");
        bGM.loop = true;
        bGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);//Camera follows player.
        //Switch between boss music and not boss music.
        if (!playingBossMusic && Data.fightingBoss)
        {
            playingBossMusic = true;
            bGM.clip = Resources.Load<AudioClip>("Sounds/CastleBossMusic");
            bGM.loop = true;
            bGM.Play();
        }
        if (playingBossMusic && !Data.fightingBoss)
        {
            playingBossMusic = false;
            bGM.clip = Resources.Load<AudioClip>("Sounds/TestMainMenu");
            bGM.loop = true;
            bGM.Play();
        }
    }
}