﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileShooterAbstract : MonoBehaviour
{
    float count;
    public float delay;
    public float initialCount;
    public bool triggered;
    public AudioSource sFXPlayer;


    // Start is called before the first frame update
    void Start()
    {
        sFXPlayer = GetComponent<AudioSource>();
        count = initialCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered)
        {
            count += Time.deltaTime;
            if (count > delay)
            {
                ShootProjectile();
                count = 0;
            }
            Move();
        }
    }

    public virtual void ShootProjectile()
    {
        sFXPlayer.Play();
    }
    public abstract void Move();
}
