using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBoss : MonoBehaviour
{
    public GameObject pollutionLeft;
    public GameObject pollutionRight;
    public GameObject oilLeft;
    public GameObject oilRight;
    private float count;
    private int phase;
    public Animator animator;
    public Enemy enemy;
    private int health;
    private int projectileNumber;
    private bool animating;
    public GameObject[] dynamites;
    public float dynamiteDelay;//Time before first dynamite is shot.
    public float dynamiteDifferance;//Time between dynamite shots.
    public float pollutionDifferance;//Time between pollution spewwing.
    public float jumpDuration;//How long the car jumps for.
    private float jumpDestination;//Stores where the car tries to land.
    public float jumpHeight;//How high the car jumps.
    private float endHeight;//Stores height of car destination.
    private float jumpStart;//Stores original jump x Position.
    public float revTime;//How long the car revvs up.
    public float driveTime;//How long it takes for the car to drive across the screen.
    public float driveDistance;//How far the car drives.
    private float projectileCount;
    public float jumpDelay;//Time before first car jump, and in between each jump.
    private float startX;//Initial x position which car returns to before driving.
    public AudioSource sFXPlayer;
    private float damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        health = enemy.maxHealth;
        jumpDestination = -1;
        jumpStart = -1;
        endHeight = transform.position.y;
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(health != enemy.health)
        {
            sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/MetalClink");
            sFXPlayer.Play();
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            damageTimer = 0.5f;
            health = enemy.health;
            if (health <= enemy.maxHealth * 2 / 3 && phase == 0)
            {
                phase = 1;
                count = 0;
                projectileNumber = 0;
                animating = false;
                Data.healthPack = true;
            }
            if (health <= enemy.maxHealth / 3 && phase == 1)
            {
                phase = 2;
                count = 0;
                projectileNumber = 0;
                animating = false;
                transform.position = new Vector3(startX, endHeight, 0);
                Data.healthPack = true;
            }
        }
        count += Time.deltaTime;
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                damageTimer = 0;
                GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }
        }
        if (phase == 0)
        {
            if (count >= dynamiteDelay)
            {
                float dynamiteCount = count - dynamiteDelay;
                if (dynamiteCount / dynamiteDifferance > dynamites.Length - 1)
                {
                    while (projectileNumber < dynamites.Length)//Fire any dynamite that hasn't been fired.
                    {
                        dynamites[projectileNumber].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                    count = 0;
                    projectileNumber = 0;
                }
                else
                {
                    if (dynamiteCount / dynamiteDifferance >= projectileNumber)
                    {
                        //Fire dynamite.
                        dynamites[projectileNumber].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                }
            }
        }
        else if(phase == 1)
        {
            if (count >= jumpDelay)
            {
                if (!animating)
                {
                    animator.SetTrigger("Jump");
                    animating = true;
                }
                float jumpCount = count - jumpDelay;
                if (jumpDestination == -1)
                {
                    jumpDestination = Data.playerPos.x;
                    jumpStart = transform.position.x;
                }
                if (jumpCount <= jumpDuration * 2 / 3)//Jump up for 2/3rd of the time.
                {
                    transform.Translate((jumpDestination - jumpStart) * Time.deltaTime / jumpDuration, jumpHeight * Time.deltaTime / (jumpDuration  * 2 / 3), 0);
                }
                else if (jumpCount <= jumpDuration)//Fall down for 1/3rd of the time.
                {
                    transform.Translate((jumpDestination - jumpStart) * Time.deltaTime / jumpDuration, jumpHeight * Time.deltaTime / (jumpDuration / -3), 0);
                }
                else
                {
                    transform.position = new Vector3(jumpDestination, endHeight);
                    //Shoot out oil when landing.
                    oilLeft.GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                    oilRight.GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                    count = 0;
                    jumpDestination = -1;
                    animating = false;
                }
            }
        }
        else if(phase == 2)
        {
            if(jumpDestination == -1)
            {
                jumpStart = transform.position.x;
                jumpDestination = transform.position.x;
                animator.SetTrigger("Rev");
                sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/CarStall");
                sFXPlayer.Play();
            }
            if (count >= revTime)
            {
                if (!animating)
                {
                    sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/CarRev");
                    sFXPlayer.Play();
                    animator.SetTrigger("Drive");//Start drive animation when we start moving.
                    animating = true;
                }
                projectileCount += Time.deltaTime;
                float revCount = count - revTime;
                if (revCount <= driveTime)//Move left for the duration.
                {
                    transform.Translate(-1 * driveDistance * Time.deltaTime / driveTime, 0, 0);
                    if(projectileCount >= pollutionDifferance)
                    {
                        pollutionRight.GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        for(int i = 0; i < dynamites.Length / 2; ++i)
                        {
                            dynamites[Random.Range(0, dynamites.Length)].GetComponent<ProjectileShooterAbstract>().ShootProjectile();//Shoot two random dynamites when pollution is spewwed.
                        }
                        projectileCount = 0;
                    }
                }
                else if(revCount <= driveTime * 2)//Move right for the duration.
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.Translate(-1 * driveDistance * Time.deltaTime / driveTime, 0, 0);
                    if (projectileCount >= pollutionDifferance)
                    {
                        pollutionLeft.GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        for (int i = 0; i < dynamites.Length; ++i)
                        {
                            dynamites[i].GetComponent<ProjectileShooterAbstract>().ShootProjectile();//Shoot all dynamites when pollution is spewwed.
                        }
                        projectileCount = 0;
                    }
                }
                else
                {
                    pollutionLeft.GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                    for (int i = 0; i < dynamites.Length; ++i)
                    {
                        dynamites[i].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                    }
                    transform.rotation = Quaternion.Euler(0, 0, 0);//Rotate back to the left.
                    transform.position = new Vector3(startX, endHeight, 0);
                    projectileCount = 0;
                    count = 0;
                    jumpDestination = -1;
                    jumpStart = -1;
                    animating = false;
                }
            }
        }
    }
}
