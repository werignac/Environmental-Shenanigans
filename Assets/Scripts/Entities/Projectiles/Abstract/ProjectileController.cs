using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour
{
    public float impactMass;
    public Vector2 moveDirection;

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        transform.Translate(moveDirection * Time.deltaTime);

        OnUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject encounter = collision.gameObject;
        if (encounter.CompareTag("Shield"))
        {
            Debug.Log("Impact");
            ShieldController shield = encounter.GetComponentInParent<ShieldController>();
            shield.Impact(moveDirection, impactMass);
            moveDirection *= -1;
        }
    }

    public abstract void OnStart();
    public abstract void OnUpdate();
}
