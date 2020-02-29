﻿using System.Collections;
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
        if ((other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Explosion")) && enemy != null && other.GetComponent<ProjectileController>().GetReflected())
        {
            Destroy(other.gameObject);
            enemy.Damage();
        }
    }
}
