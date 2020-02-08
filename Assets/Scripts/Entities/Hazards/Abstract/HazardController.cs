using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class that describes the general behavior of a hazard.
/// </summary>
public abstract class HazardController : MonoBehaviour
{
    /// <summary>
    /// <c>moveDirection</c> Is an additional direction to send the player in upon contact with the <c>Hazard</c>.
    /// For <c>Projectiles</c>, it defines the direction the player moves in.
    /// </summary>
    /// <seealso cref="ShieldController.Impact(Vector2)"/>
    /// <seealso cref="ProjectileController"/>
    public Vector2 moveDirection;
    public float mass;

    /// <summary>
    /// Sets <c>moveDirection</c> to a new direction.
    /// </summary>
    /// <param name="dir">The new direction for <c>moveDirection</c>.</param>
    /// <see cref="moveDirection"/>
    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }
    /// <summary>
    /// Returns <c>moveDirection</c>.
    /// </summary>
    /// <returns><c>moveDirection</c></returns>
    /// <see cref="moveDirection"/>
    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    /// <summary>
    /// The method used to react to a collision with the player's sheild.
    /// </summary>
    /// <param name="encounter">The player's sheild rotator.</param>
    public abstract void OnShieldCollision(GameObject encounter);
}
