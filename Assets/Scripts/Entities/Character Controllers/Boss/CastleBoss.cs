using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBoss : Enemy
{
    //All times are in seconds.
    public GameObject[] archers;
    public GameObject[] cannons;
    public GameObject[] trebuchet;
    private float count;
    private int phase;
    public float archerDelay;//The time before the first arrow.
    public float archerDifference;//The time between arrows.
    public float cannonDelay;//The time before the first cannon shot.
    public float cannonDifference;//The time between cannon shots.
    public float trebuchetDelay;//The time before the trebuchet shots.
    public int trebuchetBullets;//The number of trebuchet bullets dropped.
    private int projectileNumber;//A tracker for number of projectiles fired.
    public Animator animator;
    public float cannonAnimationStart;//When to start the cannon animation.
    public float cannonAnimationEnd;//When the cannon animation ends.
    public float trebuchetAnimationStart;//When the trebuchet animation starts.
    private bool animating;
    public AudioSource sFXPlayer;

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
                    while(projectileNumber < archers.Length)//Fire any arrows not yet fired.
                    {
                        archers[projectileNumber].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                    count = 0;
                    projectileNumber = 0;
                }
                else
                {
                    if (archerCount / archerDifference >= projectileNumber)//Fire an arrow every archerDifference seconds.
                    {
                        archers[projectileNumber].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                }
            }
        }
        else if (phase == 1)
        {
            if(count >= cannonAnimationStart && !animating)
            {
                animator.SetTrigger("Attack1");
                animating = true;//Set this so the animation doesn't loop and get offset.
            }
            if (count >= cannonDelay)
            {
                float cannonCount = count - cannonDelay;
                if (cannonCount / cannonDifference > cannons.Length - 1 && count >= cannonAnimationEnd)
                {
                    while(projectileNumber < cannons.Length)//Fire any cannonball not fired yet, to ensure all three fire.
                    {
                        cannons[projectileNumber].GetComponentInChildren<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                    }
                    count = 0;
                    projectileNumber = 0;
                    animating = false;
                }
                else
                {
                    if (cannonCount / cannonDifference >= projectileNumber && projectileNumber < cannons.Length)//Fire one cannonball every cannonDifference.
                    {
                        cannons[projectileNumber].GetComponentInChildren<ProjectileShooterAbstract>().ShootProjectile();
                        ++projectileNumber;
                        archers[Random.Range(0, archers.Length)].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                    }
                }
            }
        }
        else
        {
            if(count >= cannonAnimationStart && !animating)
            {
                animator.SetTrigger("Attack2");
                animating = true;
                sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/TrebuchetWoosh");
                sFXPlayer.Play();
            }
            if (count >= trebuchetDelay)
            {
                List<int> nums = new List<int>();
                for (int i = 0; i < trebuchet.Length; ++i)
                {
                    nums.Add(i);
                }
                for(int i = 0; i < trebuchetBullets; ++i)
                {
                    int num = nums[Random.Range(0, nums.Count)];//Chose a random Trebuchet dropper to trigger.
                    trebuchet[num].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                    nums.Remove(num);
                    archers[Random.Range(0, archers.Length)].GetComponent<ProjectileShooterAbstract>().ShootProjectile();
                }
                count = 0;
                animating = false;
            }
        }
    }

    public override void Damage()
    {
        sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/StoneCrumble");
        sFXPlayer.Play();
        base.Damage();
        if(health <= maxHealth * 2 / 3 && phase == 0)
        {
            phase = 1;
            count = 0;
            projectileNumber = 0;
            animating = false;
            Data.healthPack = true;
        }
        if(health <= maxHealth / 3 && phase == 1)
        {
            phase = 2;
            count = 0;
            projectileNumber = 0;
            animating = false;
            Data.healthPack = true;
        }
    }
}
