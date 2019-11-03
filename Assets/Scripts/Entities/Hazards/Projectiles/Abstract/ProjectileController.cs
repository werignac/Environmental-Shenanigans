using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : HazardController
{
    public Vector2 moveDir;
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
            SetMoveDirection(rigid.velocity);
        }

        OnUpdate();
    }

    public override void OnShieldCollision(GameObject encounter)
    {
        float shieldAngle = encounter.GetComponentInParent<Transform>().eulerAngles.z;
        float projectAngle = Mathf.Atan2(moveDirection.y, moveDirection.x)*Mathf.Rad2Deg;
        float diff = Mathf.DeltaAngle(projectAngle, shieldAngle);
        float finalAngle = shieldAngle + diff;

        SetMoveDirection(new Vector2(Mathf.Cos(finalAngle)*moveDirection.magnitude, Mathf.Sin(finalAngle) * moveDirection.magnitude));
    }

    public abstract void OnStart();
    public abstract void OnUpdate();
}
