﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float rotationOffset;

    private void RotateTowards(Vector2 point)
    {
        float xDiff = point.x - transform.position.x;
        float yDiff = point.y - transform.position.y;
        float angle = Mathf.Atan2(yDiff, xDiff)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0,angle);
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePos);
        RotateTowards(mousePos);
        transform.Rotate(new Vector3(0, 0, rotationOffset));
    }

}
