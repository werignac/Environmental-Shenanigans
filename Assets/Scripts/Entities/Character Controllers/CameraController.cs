using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AudioSource bGM;
    private GameObject player;
    private bool playingBossMusic;

    public bool lockX;
    public bool lockY;

    public Vector2 buffer = new Vector2(2.5f,2.5f);

    public float moveSpeed = 1;
    private float moveTime = 1;

    public float timeOut = 5;
    private float timeOutTimer;

    private float goalX;

    private Vector2 lastPlayerPos;
    private float lastVel;
    private Rigidbody2D pRigid;

    public float changeVMin = 5;


    // Start is called before the first frame update
    void Start()
    {
        playingBossMusic = false;
        Data.fightingBoss = false;
        bGM.clip = Resources.Load<AudioClip>("Sounds/TestMainMenu");
        bGM.loop = true;
        bGM.Play();

        moveTime = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            pRigid = player.GetComponent<Rigidbody2D>();
            lastPlayerPos = player.transform.position;
        }

        Vector2 playerPos = player.transform.position;

        Vector3 newPosition = transform.position;

        float posDiff = lastPlayerPos.x - playerPos.x;

        Vector2 diff = ((Vector2) newPosition) - playerPos;

        if (!lockX)
        {
            if (pRigid.velocity.x != 0 && moveTime != moveSpeed)
            {
                if (pRigid.velocity.x < 0)
                {
                    goalX = playerPos.x - buffer.x*0.6f;

                    if (lastVel >= 0)
                    { 
                        moveTime = 0;
                    }

                    newPosition.x = Mathf.Lerp(transform.position.x, goalX, moveTime / moveSpeed);
                }
                else if (pRigid.velocity.x > 0)
                {
                    goalX = playerPos.x + buffer.x*0.6f;
                    
                    if (lastVel <= 0)
                    {
                        moveTime = 0;
                    }

                    newPosition.x = Mathf.Lerp(transform.position.x, goalX, moveTime / moveSpeed);
                }
            }
            else if (diff.x < -buffer.x)
            {
                if (moveTime == moveSpeed)
                {
                    moveTime = 0;
                }
            }
            else if (diff.x > buffer.x)
            {
                if (moveTime == moveSpeed)
                {
                    moveTime = 0;
                }

            }
        }

        if (! lockY)
        {
            if (diff.y < -buffer.y)
            {
                newPosition.y = playerPos.y - buffer.y;
            }
            else if (diff.y > buffer.y)
            {
                newPosition.y = playerPos.y + buffer.y;
            }
        }

        if (moveTime != moveSpeed)
        {
            if (posDiff == 0)
            {
                moveTime = Mathf.Min(moveTime + Time.deltaTime, moveSpeed);
            }
            else
            {
                moveTime = Mathf.Min(moveTime + Time.deltaTime, moveSpeed*0.9f);
            }

            timeOutTimer = 0;
        }
        else if (timeOutTimer < timeOut + moveSpeed)
        {
            timeOutTimer += Time.deltaTime;
            if (timeOutTimer > timeOut)
            {
                newPosition.x = Mathf.Lerp(transform.position.x, playerPos.x, (timeOutTimer - timeOut) / moveSpeed);
            }
        }

        transform.position = newPosition;//Camera follows player.

        lastPlayerPos = playerPos;
        lastVel = pRigid.velocity.x;

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