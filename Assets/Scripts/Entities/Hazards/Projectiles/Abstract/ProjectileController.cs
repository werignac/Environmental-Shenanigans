using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that describes the general behavior of <c>Projectiles</c>.
/// </summary>
public abstract class ProjectileController : MovingHazardController
{
    /// <summary>
    /// The ways projectiles can rotate.
    /// </summary>
    public enum RotationType
    {
        NOROT, FORWARDROT, CONSTANTROT
    }

    public float range;
    private float distance;
    private Vector3 pastPos;
    public RotationType rotType;
    public float angularVelocity;
    private bool reflected = false;
    public string deathSummonName;
    /// <summary>
    /// Switches the <c>Projectile</c>'s direction if it encounters a reflecting surface. 
    /// </summary>
    /// <param name="encounter">The reflecting surface.</param>
    public override void OnShieldCollision(GameObject encounter)
    {
        reflected = true;

        float shieldAngle = encounter.GetComponent<Transform>().eulerAngles.z;
        Vector2 projectileMove = GetMoveDirection();
        float projectileAngle = Mathf.Atan2(projectileMove.y, projectileMove.x) * Mathf.Rad2Deg;
        while(projectileAngle < 0)
        {
            projectileAngle += 360;
        }
        shieldAngle *= Mathf.Deg2Rad;
        SetMoveDirection(new Vector2(Mathf.Cos(shieldAngle), Mathf.Sin(shieldAngle)) * projectileMove.magnitude);
        /*if(Mathf.Abs(projectileAngle - shieldAngle) <= 90 || Mathf.Abs(projectileAngle - shieldAngle) >= 270)
        {
            shieldAngle *= Mathf.Deg2Rad;
            SetMoveDirection(new Vector2(Mathf.Cos(shieldAngle), Mathf.Sin(shieldAngle)) * projectileMove.magnitude);
            return;
        }
        projectileAngle -= shieldAngle;
        projectileAngle += 180;
        while(projectileAngle <= -180)
        {
            projectileAngle += 360;
        }
        while(projectileAngle > 180)
        {
            projectileAngle -= 360;
        }
        projectileAngle *= -1;
        projectileAngle += shieldAngle;
        while (projectileAngle <= -180)
        {
            projectileAngle += 360;
        }
        while (projectileAngle > 180)
        {
            projectileAngle -= 360;
        }
        projectileAngle *= Mathf.Deg2Rad;
        SetMoveDirection(new Vector2(Mathf.Cos(projectileAngle), Mathf.Sin(projectileAngle)) * projectileMove.magnitude);*/
    }

    public bool GetReflected()
    {
        return reflected;
    }

    private static int GetQuad(float angle)
    {
        if (angle > 0)
        {
            if (angle > 90)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            if (angle < -90)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
    }

    private static int GetQuad(float x, float y)
    {
        if (x > 0)
        {
            if (y > 0)
            {
                return 1;
            }
            else
            {
                return 4;
            }
        }
        else
        {
            if (y > 0)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }

    public float getDistance()
    {
        return distance;
    }


    public override void OnStart()
    {
        pastPos = new Vector3(transform.position.x, transform.position.y, 0);
        distance = 0;
        setRot();
    }

    public void setRot()
    {
        switch (rotType)
        {
            case RotationType.CONSTANTROT:
                transform.Rotate(new Vector3(0, 0, angularVelocity * Time.deltaTime));
                break;
            case RotationType.FORWARDROT:
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg);
                break;
        }
    }

    public override void OnUpdate()
    {
        distance += (transform.position - pastPos).magnitude;
        pastPos = new Vector3(transform.position.x, transform.position.y, 0);

        setRot();

        if (distance > range)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        if (deathSummonName != null && deathSummonName != "")
        {
            GameObject g = Instantiate(Resources.Load<GameObject>("Projectiles/" + deathSummonName), transform.position, new Quaternion());
            g.GetComponent<ProjectileController>().reflected = reflected;
        }
        Destroy(gameObject);
    }
}
