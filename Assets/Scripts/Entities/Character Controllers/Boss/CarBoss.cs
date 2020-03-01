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
    public float dynamiteDelay;
    public float dynamiteDifferance;
    public float pollutionDifferance;
    public float jumpDuration;
    private float jumpDestination;
    public float jumpHeight;
    private float endHeight;
    private float jumpStart;
    public float revTime;
    public float driveTime;
    public float driveDistance;
    private float projectileCount;
    public float jumpDelay;
    private float startX;

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
            health = enemy.health;
            if (health < enemy.maxHealth * 2 / 3)
            {
                phase = 1;
                count = 0;
                projectileNumber = 0;
                animating = false;
            }
            if (health < enemy.maxHealth / 3)
            {
                phase = 2;
                count = 0;
                projectileNumber = 0;
                animating = false;
                transform.position = new Vector3(startX, endHeight, 0);
            }
        }
        count += Time.deltaTime;
        if (phase == 0)
        {
            if (count >= dynamiteDelay)
            {
                float dynamiteCount = count - dynamiteDelay;
                if (dynamiteCount / dynamiteDifferance > dynamites.Length - 1)
                {
                    while (projectileNumber < dynamites.Length)
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
                if (jumpCount <= jumpDuration * 2 / 3)
                {
                    transform.Translate((jumpDestination - jumpStart) * Time.deltaTime / jumpDuration, jumpHeight * Time.deltaTime / (jumpDuration  * 2 / 3), 0);
                }
                else if (jumpCount <= jumpDuration)
                {
                    transform.Translate((jumpDestination - jumpStart) * Time.deltaTime / jumpDuration, jumpHeight * Time.deltaTime / (jumpDuration / -3), 0);
                }
                else
                {
                    transform.position = new Vector3(jumpDestination, endHeight);
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
            }
            if (count >= revTime)
            {
                if (!animating)
                {
                    animator.SetTrigger("Drive");
                    animating = true;
                }
                projectileCount += Time.deltaTime;
                float revCount = count - revTime;
                if (revCount <= driveTime)
                {
                    transform.Translate(-1 * driveDistance * Time.deltaTime / driveTime, 0, 0);
                    if(projectileCount >= pollutionDifferance)
                    {
                        pollutionRight.GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        for(int i = 0; i < dynamites.Length / 2; ++i)
                        {
                            dynamites[Random.Range(0, dynamites.Length)].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        }
                        projectileCount = 0;
                    }
                }
                else if(revCount <= driveTime * 2)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.Translate(-1 * driveDistance * Time.deltaTime / driveTime, 0, 0);
                    if (projectileCount >= pollutionDifferance)
                    {
                        pollutionLeft.GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        for (int i = 0; i < dynamites.Length; ++i)
                        {
                            dynamites[i].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
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
                    transform.rotation = Quaternion.Euler(0, 0, 0);
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
