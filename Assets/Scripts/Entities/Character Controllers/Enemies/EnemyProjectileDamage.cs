using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileDamage : MonoBehaviour
{
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< Updated upstream
        if (other.gameObject.CompareTag("Projectile") && enemy != null)
=======
        if (other.gameObject.CompareTag("Projectile") && enemy != null && other.GetComponent<ProjectileController>().GetReflected())
>>>>>>> Stashed changes
        {
            Destroy(other.gameObject);
            enemy.Damage();
        }
    }
}
