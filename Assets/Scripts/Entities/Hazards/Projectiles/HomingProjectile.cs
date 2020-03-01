using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : AcceleratingProjectileController
{
    private float accelerationMag;//How fast it accelerates.
    private Transform target;
    public float targetDist;

    public override void OnStart()
    {
        base.OnStart();
        accelerationMag = acceleration.magnitude;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnUpdate()
    {
        if (getDistance() < targetDist)//If it hasn't traveled the target distance yet it accelerates towards player, otherwise it maintains speed.
        {
            Vector2 difference = target.position - transform.position;
            float ratio = accelerationMag / difference.magnitude;
            acceleration = new Vector2(ratio * difference.x, ratio * difference.y);
        }

        

        base.OnUpdate();
    }
}
