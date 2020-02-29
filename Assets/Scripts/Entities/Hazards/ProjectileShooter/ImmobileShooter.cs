using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmobileShooter : ProjectileShooterAbstract
{
    public string projectileName;
    public float angle;
    public float speed;
    public float range;
    public float angleVariation;
    public override void Move()
    {
        return;
    }

    public override void ShootProjectile()
    {
        base.ShootProjectile();
        GameObject projectile = Instantiate(Resources.Load<GameObject>("Projectiles/" + projectileName), transform.position, new Quaternion());
        projectile.GetComponent<ProjectileController>().SetMoveDirection(new Vector2(speed * Mathf.Cos((transform.rotation.eulerAngles.x + angle) * Mathf.Deg2Rad), speed * Mathf.Sin((transform.rotation.eulerAngles.x + angle) * Mathf.Deg2Rad)));
        projectile.GetComponent<ProjectileController>().range = range;
        if (givesAcceleration)
        {
            AcceleratingProjectileController a = projectile.GetComponent<AcceleratingProjectileController>();
            if(a != null)
            {
                a.acceleration = acceleration;
            }
        }
    }
}
