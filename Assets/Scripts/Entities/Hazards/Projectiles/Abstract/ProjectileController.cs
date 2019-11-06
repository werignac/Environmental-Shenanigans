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
        float projectAngle = Mathf.Atan2(GetMoveDirection().y, GetMoveDirection().x)*Mathf.Rad2Deg;
        float diff = Mathf.DeltaAngle(projectAngle, shieldAngle);
        float finalAngle = shieldAngle + diff;

        SetMoveDirection(new Vector2(Mathf.Cos(finalAngle)* GetMoveDirection().magnitude, Mathf.Sin(finalAngle) * GetMoveDirection().magnitude));
    }
}
