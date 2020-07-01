using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratingVaccumProjectile : AcceleratingProjectile
{
    public override void OnShieldCollision(GameObject encounter)
    {
        base.OnShieldCollision(encounter);
        SetMoveDirection(new Vector2(GetMoveDirection().x * -1, GetMoveDirection().y * -1));
    }
}
