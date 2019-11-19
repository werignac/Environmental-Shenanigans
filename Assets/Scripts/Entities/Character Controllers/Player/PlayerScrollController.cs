using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScrollController : PlayerController
{
    /// <summary>
    /// The horizontal velocity of the player for purposes of scrolling.
    /// </summary>
    private float velocity;

    /// <summary>
    /// Moves the player by applying a vertical force or changing the velocity.
    /// </summary>
    /// <param name="horizontal">The horizontal force.</param>
    /// <param name="vertical">The vertical force.</param>
    public override void Move(float horizontal, float vertical)
    {
        base.rigid.velocity = new Vector2(velocity, base.rigid.velocity.y);

        rigid.AddForce(new Vector2(horizontal * accelCoeff.x, vertical * accelCoeff.y), ForceMode2D.Impulse);
        float currentSpeed = Mathf.Abs(rigid.velocity.x);
        if (currentSpeed > maxSpeedX)
        {
            rigid.velocity = new Vector2(rigid.velocity.x * maxSpeedX / currentSpeed, rigid.velocity.y);
        }
        currentSpeed = Mathf.Abs(rigid.velocity.y);
        if (currentSpeed > maxSpeedY)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * maxSpeedY / currentSpeed);
        }

        velocity = base.rigid.velocity.x;
        Debug.Log(base.rigid.velocity.x);
        base.rigid.velocity = new Vector2(0, base.rigid.velocity.y);
        transform.position = new Vector3(0, transform.position.y);
        if(velocity > 0)
        {
            velocity = Mathf.Max(0, velocity - 0.04f);
        }
        if (velocity < 0)
        {
            velocity = Mathf.Min(0, velocity + 0.04f);
        }
    }

    /// <summary>
    /// Allows access to the velocity.
    /// </summary>
    /// <returns>Returns the current velocity value.</returns>
    public float GetVelocity()
    {
        return (velocity);
    }
}
