using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileShooterAbstract : MonoBehaviour
{
    float count;
    public float delay;
    public float initialCount;
    public bool triggered;
    public AudioSource sFXPlayer;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
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
        if (sFXPlayer != null)
        {
            sFXPlayer.Play();
        }
        
        if(animator != null)
        {
            animator.SetTrigger("Fire");
        }
    }
    public abstract void Move();
    public virtual void OnStart()
    {
        sFXPlayer = GetComponent<AudioSource>();
        count = initialCount;
    }
}
