using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that checks to see if it was hit by a projectile.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class HitArea : MonoBehaviour
{
    /// <summary>
    /// The health points associated with the entity.
    /// </summary>
    public HealthPoints healthPoints;

    /// <summary>
    /// Tells the health points when the entity has been hit.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile") && healthPoints != null)
        {
            Damage();
        }
    }

    public void Damage()
    {
        healthPoints.Hit();
    }
}
