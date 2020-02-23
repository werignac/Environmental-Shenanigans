using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBoss : Enemy
{
    public GameObject[] archers;
    public GameObject[] cannons;
    public GameObject[] trebuchet;
    private int count;
    private int phase;
    public float archerDelay;
    public float archerDifference;
    public float cannonDelay;
    public float cannonDifference;
    public float trebuchetDelay;
    public int trebuchetBullets;
    private int projectileNumber;
    public override void Move()
    {
        ++count;
        if (phase == 0)
        {
            if (count >= (int)(archerDelay * Data.frameRate))
            {
                int archerCount = count - (int)(archerDelay * Data.frameRate);
                int aDelay = (int)(archerDifference * Data.frameRate);
                if (archerCount / aDelay >= archers.Length - 1)
                {
                    count = 0;
                    projectileNumber = 0;
                }
                else
                {
                    if (archerCount / aDelay >= projectileNumber)
                    {
                        archers[projectileNumber].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                }
            }
        }
        else if (phase == 1)
        {
            if (count >= (int)(cannonDelay * Data.frameRate))
            {
                int cannonCount = count - (int)(cannonDelay * Data.frameRate);
                int cDelay = (int)(cannonDifference * Data.frameRate);
                if (cannonCount / cDelay >= cannons.Length - 1)
                {
                    count = 0;
                    projectileNumber = 0;
                }
                else
                {
                    if (cannonCount / cDelay >= projectileNumber)
                    {
                        cannons[projectileNumber].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                }
        }
        else
        {
            if (count >= (int)(trebuchetDelay * Data.frameRate))
            {
                List<int> nums = new List<int>();
                for (int i = 0; i < trebuchet.Length; ++i)
                {
                    nums.Add(i);
                }
                for(int i = 0; i < trebuchetBullets; ++i)
                {
                    int num = nums[Random.Range(0, nums.Count)];
                    trebuchet[num].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                    nums.Remove(num);
                }
            }
        }
    }

    public override void Damage()
    {
        base.Damage();
        if(health < maxHealth * 2 / 3)
        {
            phase = 1;
            count = 0;
            projectileNumber = 0;
        }
        if(health < maxHealth / 3)
        {
            phase = 2;
            count = 0;
            projectileNumber = 0;
        }
    }
}
