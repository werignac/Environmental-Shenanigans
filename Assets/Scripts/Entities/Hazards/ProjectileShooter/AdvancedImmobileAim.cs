using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedImmobileAim : ImmobileShooter
{
    private bool accelerating;
    private float accelerate;
    public override void ShootProjectile()
    {
        float x = Data.playerPos.x - transform.position.x;
        float y = Data.playerPos.y - transform.position.y;
        if (!accelerating)//If projectile doesn't accelerate, fire directly at player.
        {
            angle = Mathf.Rad2Deg * Mathf.Atan((Data.playerPos.y - transform.position.y) / (Data.playerPos.x - transform.position.x));
            if (Data.playerPos.x < transform.position.x)
            {
                angle += 180;
            }
        }
        else
        {
            /*float cos = Mathf.Acos(Mathf.Sqrt(Mathf.Abs(y * Mathf.Pow(speed, 2) / (p.acceleration.y * Mathf.Pow(x, 2)))));
            if (cos != 0)
            {
                angle = Mathf.Rad2Deg / cos;
                if (Data.playerPos.y < transform.position.y)
                {
                    angle += 90;
                }
            }
            else angle = 90;*/
            if (Mathf.Abs(x * accelerate) > Mathf.Abs(Mathf.Pow(speed, 2)))//When the player is too far away to hit it fires up at a 45 degree angle for maximum distance.
            {
                angle = 45;
            }
            else
            {
                angle = Mathf.Rad2Deg * Mathf.Asin(x * accelerate / Mathf.Pow(speed, 2)); //This is using the range equation, assuming the player is at the same height as the projectile shooter.
            }
            if(Data.playerPos.x < transform.position.x)
            {
                angle += 90;
            }
        }
        angle += Random.Range(angleVariation * -1, angleVariation);//Use angle variation for more interesting fights, where you don't just stand still and point at the boss.
        base.ShootProjectile();
    }

    public override void OnStart()
    {
        base.OnStart();
        AcceleratingProjectile p = Resources.Load<GameObject>("Projectiles/" + projectileName).GetComponent<AcceleratingProjectile>();
        if (p == null)
        {
            accelerating = true;
            accelerate = p.acceleration.y;
        }
        else
        {
            accelerating = false;
            accelerate = 0;
        }
    }
}
