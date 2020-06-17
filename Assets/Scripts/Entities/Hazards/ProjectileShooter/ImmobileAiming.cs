using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmobileAiming : ImmobileShooter
{
    //Get the angle towards player, then use base class fire method.
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
