using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AudioSource bGM;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
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
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}