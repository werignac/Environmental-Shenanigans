using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the behavior of a general projectile that is accelerating in a given direction.
/// </summary>
public abstract class AcceleratingProjectileController : ProjectileController
{
    /// <summary>
    /// The direction the projectile accelerates in.
    /// </summary>
    public Vector2 acceleration;

    public override void OnUpdate()
    {
        SetMoveDirection(GetMoveDirection() + acceleration * Time.deltaTime);
    }
}
