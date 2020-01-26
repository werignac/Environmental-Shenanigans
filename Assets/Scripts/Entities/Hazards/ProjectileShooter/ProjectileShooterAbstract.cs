using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileShooterAbstract : MonoBehaviour
{
    int count;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ++count;
        if(count > (delay * Data.frameRate))
        {
            ShootProjectile();
            count = 0;
        }
        Move();
    }

    public abstract void ShootProjectile();
    public abstract void Move();
}
