using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AudioSource bGM;
    // Start is called before the first frame update
    void Start()
    {
        bGM.clip = Resources.Load<AudioClip>("Sounds/Masked Dedede Remix - Hardcore");
        bGM.loop = true;
        bGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, transform.position.z);
    }
}