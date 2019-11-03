using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that describes the general behavior of <c>Projectiles</c>.
/// </summary>
public abstract class ProjectileController : HazardController
{
    /// <summary>
    /// Instantiates the <c>Projectile</c> (if necessary).
    /// </summary>
    private void Start()
    {
        OnStart();
    }

    /// <summary>
    /// Moves the <c>Projectile</c> every frame.
    /// </summary>
    private void Update()
    {
        transform.Translate(GetMoveDirection() * Time.deltaTime);
        OnUpdate();
    }

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

    /// <summary>
    /// Does something at the beginning of the game.
    /// </summary>
    public abstract void OnStart();
    /// <summary>
    /// Does something at every frame.
    /// </summary>
    public abstract void OnUpdate();
}
