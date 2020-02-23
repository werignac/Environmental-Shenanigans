using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandShootEnemy : Enemy
{
    public int delay;
    public float initialCount;
    public string projectileName;
    public int speed;
    public int range;
    private int count;

    public override void OnStart()
    {
        base.OnStart();
        count = (int)(delay * Data.frameRate) - (int)(initialCount * Data.frameRate);
    }
    public override void Move()
    {
        --count;
        if(count <= 0)
        {
            GameObject projectile = Instantiate(Resources.Load<GameObject>("Projectiles/" + projectileName), transform.position, new Quaternion());
            float angle = Mathf.Rad2Deg * Mathf.Atan((Data.playerPos.y - transform.position.y) / (Data.playerPos.x - transform.position.x));
            if(Data.playerPos.x < transform.position.x)
            {
                angle += 180;
            }
            projectile.GetComponent<ProjectileController>().SetMoveDirection(new Vector2(speed * Mathf.Cos((transform.rotation.eulerAngles.x + angle) * Mathf.Deg2Rad), speed * Mathf.Sin((transform.rotation.eulerAngles.x + angle) * Mathf.Deg2Rad)));
            projectile.GetComponent<ProjectileController>().range = range;
            count = (int)(delay * Data.frameRate);
        }
    }
}
