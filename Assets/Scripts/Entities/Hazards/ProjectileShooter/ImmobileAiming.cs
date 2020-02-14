using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmobileAiming : ImmobileShooter
{
    public override void ShootProjectile()
    {
        angle = Mathf.Rad2Deg * Mathf.Atan((Data.playerPos.y - transform.position.y) / (Data.playerPos.x - transform.position.x));
        if (Data.playerPos.x < transform.position.x)
        {
            angle += 180;
        }
        base.ShootProjectile();
    }
}
