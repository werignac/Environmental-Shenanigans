using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileDamage : MonoBehaviour
{
    public Enemy enemy;

    public BossHealthPoints bossPoints; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Explosion")) && enemy != null && other.GetComponent<ProjectileController>().GetReflected())//If projectile is reflected deal one damage.
        {
            Destroy(other.gameObject);
            enemy.Damage();
            if (bossPoints != null)
            {
                bossPoints.RemoveHitPoint();
            }
        }
    }
}
