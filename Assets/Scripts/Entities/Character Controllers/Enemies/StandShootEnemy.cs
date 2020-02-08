using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandShootEnemy : Enemy
{
    public int delay;
    public string projectileName;
    public int speed;
    public int range;
    private int count;

    public override void OnStart()
    {
        base.OnStart();
        count = delay;
    }
    public override void Move()
    {
        if(count <= 0)
        {
            GameObject projectile = Instantiate(Resources.Load<GameObject>("Projectiles/" + projectileName), transform.position, new Quaternion());
            float angle = Mathf.Atan((Data.playerPos.y - transform.position.y) / (Data.playerPos.x - transform.position.x));
            projectile.GetComponent<ProjectileController>().SetMoveDirection(new Vector2(speed * Mathf.Cos((transform.rotation.eulerAngles.x + angle) * Mathf.Deg2Rad), speed * Mathf.Sin((transform.rotation.eulerAngles.x + angle) * Mathf.Deg2Rad)));
            projectile.GetComponent<ProjectileController>().range = range;
        }
    }
}
