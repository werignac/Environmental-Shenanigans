using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedImmobileAim : ImmobileShooter
{
    public override void ShootProjectile()
    {
        float x = Data.playerPos.x - transform.position.x;
        float y = Data.playerPos.y - transform.position.y;
        AcceleratingProjectile p = Resources.Load<GameObject>("Projectiles/" + projectileName).GetComponent<AcceleratingProjectile>();
        if (p == null)
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
            if (Mathf.Abs(x * p.acceleration.y) > Mathf.Abs(Mathf.Pow(speed, 2)))
            {
                angle = 45;
            }
            else
            {
                angle = Mathf.Rad2Deg * Mathf.Asin(x * p.acceleration.y / Mathf.Pow(speed, 2));
            }
            if(Data.playerPos.x < transform.position.x)
            {
                angle += 90;
            }
        }
        angle += Random.Range(angleVariation * -1, angleVariation);
        base.ShootProjectile();
    }
}
