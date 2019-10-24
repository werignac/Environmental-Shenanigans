using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour
{
    public float impactMass;
    public Vector2 moveDirection;
    private bool rigidBodyControl;
    private bool hitShield;
    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigidBodyControl = rigid != null;
        OnStart();
    }

    private void Update()
    {
        if (!rigidBodyControl)
        {
            transform.Translate(moveDirection * Time.deltaTime);
        }
        else
        {
            moveDirection = rigid.velocity;
        }

        OnUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject encounter = collision.gameObject;
        if (! hitShield && encounter.CompareTag("Shield"))
        {
            ShieldController shield = encounter.GetComponentInParent<ShieldController>();
            shield.Impact(moveDirection);

            float shieldAngle = encounter.GetComponentInParent<Transform>().eulerAngles.z;
            float projectAngle = Mathf.Atan2(moveDirection.y, moveDirection.x)*Mathf.Rad2Deg;
            float diff = Mathf.DeltaAngle(projectAngle, shieldAngle);
            float finalAngle = shieldAngle + diff;

            moveDirection = new Vector2(Mathf.Cos(finalAngle)*moveDirection.magnitude, Mathf.Sin(finalAngle) * moveDirection.magnitude);

            hitShield = true;
        }
    }

    public abstract void OnStart();
    public abstract void OnUpdate();
}
