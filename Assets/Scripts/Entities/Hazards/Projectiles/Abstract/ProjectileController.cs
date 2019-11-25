using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that describes the general behavior of <c>Projectiles</c>.
/// </summary>
public abstract class ProjectileController : MovingHazardController
{
    /// <summary>
    /// Switches the <c>Projectile</c>'s direction if it encounters a reflecting surface. 
    /// </summary>
    /// <param name="encounter">The reflecting surface.</param>
    public override void OnShieldCollision(GameObject encounter)
    {
        float shieldAngle = encounter.GetComponent<Transform>().eulerAngles.z;
        Vector2 moveDir = GetMoveDirection();
        float projectAngle = Mathf.Atan2(moveDir.y, moveDir.x)*Mathf.Rad2Deg;
        if (moveDir.x < 0)
        {
            if (moveDir.y > 0)
            {
                projectAngle = Mathf.Abs(projectAngle) + 90;
            }
            else
            {
                projectAngle = -projectAngle - 90;
            }
        }
        Debug.Log(name + ": " + projectAngle);
        float diff = Mathf.DeltaAngle(Mathf.Abs(projectAngle) % 90, Mathf.Abs(shieldAngle) % 90);
        float finalAngle;
        if (projectAngle < shieldAngle)
        {
             finalAngle = shieldAngle + diff;
        }
        else
        {
            finalAngle = shieldAngle - diff;
        }

        SetMoveDirection(new Vector2(Mathf.Cos(finalAngle)* GetMoveDirection().magnitude, Mathf.Sin(finalAngle) * GetMoveDirection().magnitude));
    }
}
