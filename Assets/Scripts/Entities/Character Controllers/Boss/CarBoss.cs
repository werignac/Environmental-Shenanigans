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

    // Start is called before the first frame update
    void Start()
    {
        health = enemy.maxHealth;
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
    }
}
