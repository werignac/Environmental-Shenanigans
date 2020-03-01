using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileDamage : MonoBehaviour
{
    public Enemy enemy;

    public BossHealthPoints bossPoints;
    private float damageCount;

    public void Update()
    {
        if(damageCount > 0)
        {
            damageCount -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Explosion")) && enemy != null && other.GetComponent<ProjectileController>().GetReflected() && damageCount <= 0)//If projectile is reflected deal one damage.
        {
            if (other.gameObject.CompareTag("Projectile"))
            {
                other.GetComponent<ProjectileController>().Destroy();
            }
            enemy.Damage();
            damageCount = 0.5f;
            if (bossPoints != null)
            {
                bossPoints.RemoveHitPoint();
            }
        }
    }
}
