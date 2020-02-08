using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int maxHealth;
    private int health;
    public int maxSpeedX;
    public Rigidbody2D rigid;
    public Vector2 accelCoeff;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public abstract void Move();

    public virtual void OnStart()
    {
        rigid = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    public void Damage()
    {
        --health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
