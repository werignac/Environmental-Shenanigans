using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthPoints : HealthPoints
{
    public Vector2 maxSize;

    public int healthIntervals;

    public int currentHealth;

    public new void Start()
    {
        base.Start();
        maxSize = hitPoints[0].transform.localScale;
        currentHealth = healthIntervals;
    }

    public new void RemoveHitPoint()
    {
        currentHealth--;
        if (currentHealth != 0)
        {
            Vector2 scale = currentHealth * maxSize / healthIntervals;
            hitPoints[divider].transform.localScale = scale;
        }
        else
        {
            hitPoints[divider].SetActive(false);
            divider--;
            currentHealth = healthIntervals;
        }
    }


    public override void NoHealth(){}
}
