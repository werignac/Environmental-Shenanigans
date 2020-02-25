using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBoss : Enemy
{
    public GameObject[] archers;
    public GameObject[] cannons;
    public GameObject[] trebuchet;
    private float count;
    private int phase;
    public float archerDelay;
    public float archerDifference;
    public float cannonDelay;
    public float cannonDifference;
    public float trebuchetDelay;
    public int trebuchetBullets;
    private int projectileNumber;
    public Animator animator;

    public override void Move()
    {
        count += Time.deltaTime;
        if (phase == 0)
        {
            if (count >= archerDelay)
            {
                float archerCount = count - archerDelay;
                if (archerCount / archerDifference > archers.Length - 1)
                {
                    while(projectileNumber < archers.Length)
                    {
                        archers[projectileNumber].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                    count = 0;
                    projectileNumber = 0;
                }
                else
                {
                    if (archerCount / archerDifference >= projectileNumber)
                    {
                        archers[projectileNumber].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                }
            }
        }
        else if (phase == 1)
        {
            if (count >= cannonDelay)
            {
                float cannonCount = count - cannonDelay;
                if (cannonCount / cannonDifference > cannons.Length - 1)
                {
                    while(projectileNumber < cannons.Length)
                    {
                        cannons[projectileNumber].GetComponentInChildren<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                    count = 0;
                    projectileNumber = 0;
                    animator.SetTrigger("Attack1");
                }
                else
                {
                    if (cannonCount / cannonDifference >= projectileNumber)
                    {
                        cannons[projectileNumber].GetComponentInChildren<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                }
            }
        }
        else
        {
            if (count >= trebuchetDelay)
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
                count = 0;
                animator.SetTrigger("Attack2");
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
            animator.SetTrigger("Attack1");
        }
        if(health < maxHealth / 3)
        {
            phase = 2;
            count = 0;
            projectileNumber = 0;
            animator.SetTrigger("Attack2");
        }
    }
}
