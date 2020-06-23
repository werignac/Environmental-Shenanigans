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
    /// Whether the area detects reflected or non-reflected projectiles.
    /// </summary>
    public bool reflectOnly;
    /// <summary>
    /// Tells the health points when the entity has been hit.
    /// </summary>
    public AudioSource sFXPlayer;

    private void OnTriggerEnter2D(Collider2D other)


    {
        GameObject collidee = other.gameObject;
        if ((collidee.CompareTag("Projectile") || collidee.CompareTag("Explosion")) && healthPoints != null && collidee.GetComponent<ProjectileController>().GetReflected() == reflectOnly)//Damage when hit by unreflected projectiles and explosions.
        {
            Damage();
        }
        else if (collidee.CompareTag("Health"))
        {
            Heal(collidee.gameObject.GetComponent<HealthPack>().heal);
            Destroy(collidee.gameObject);
        }
    }

    public void Damage()
    {
        if (healthPoints != null)
        {
            if (healthPoints.Hit())
            {
                sFXPlayer.Play();
            }
        }
    }

    public void Heal(int n)
    {
        if (healthPoints != null)
        {
            healthPoints.AddHitPoints(n);
        }
    }

    public void SetHealth(int h)
    {
        healthPoints.SetHealth(h);
    }
}
