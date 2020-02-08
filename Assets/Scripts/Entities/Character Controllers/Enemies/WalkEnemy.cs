using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : Enemy
{
    public override void Move()
    {
        float horizontal = -1;
        float xSpeed = rigid.velocity.x;
        if (xSpeed > maxSpeedX)
        {
            xSpeed = maxSpeedX;
        }
        else if (xSpeed < maxSpeedX * -1)
        {
            xSpeed = maxSpeedX * -1;
        }
        if (xSpeed != 0 && horizontal / xSpeed > 0)
        {
            horizontal *= Mathf.Pow(maxSpeedX - Mathf.Abs(xSpeed), 0.1f);
        }
        rigid.AddForce(new Vector2(horizontal * accelCoeff.x, 0));
    }
}
