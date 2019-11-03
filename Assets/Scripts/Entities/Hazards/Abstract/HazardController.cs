using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HazardController : MonoBehaviour
{
    public Vector2 moveDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject encounter = collision.gameObject;
        if (encounter.CompareTag("Shield"))
        {
            ShieldController shield = encounter.GetComponentInParent<ShieldController>();
            shield.Impact(moveDirection);

            OnShieldCollision(encounter);
        }
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    public abstract void OnShieldCollision(GameObject encounter);
}
