using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that controls the main character's movements.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// The rigidbody of the main character.
    /// </summary>
    public Rigidbody2D rigid;
    /// <summary>
    /// The acceleration of the main character for horizontal and vertical movement.
    /// </summary>
    public Vector2 accelCoeff;
    /// <summary>
    /// Whether the main character is on the ground or not.
    /// </summary>
    private bool onGround;
    /// <summary>
    /// The maximum horizontal velocity the main character can reach.
    /// </summary>
    public float maxSpeedX;
    /// <summary>
    /// The maximum vertical velocity the main character can reach.
    /// </summary>
    public float maxSpeedY;

    /// <summary>
    /// Prepares the main character for the game.
    /// </summary>
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Moves the main character based on the player's input.
    /// </summary>
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        float vertical = 0;
        if (onGround) //Check for ground.
        {
            vertical = Input.GetAxis("Vertical");
            onGround = false;
        }
        Move(horizontal, vertical);
    }

    /// <summary>
    /// Moves the player by applying forces to the rigidbody.
    /// </summary>
    /// <param name="horizontal">The horizontal force to be applied.</param>
    /// <param name="vertical">The vertical force to be applied.</param>
    public virtual void Move(float horizontal, float vertical)
    {

        rigid.AddForce(new Vector2(horizontal * accelCoeff.x, vertical * accelCoeff.y));

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
    }


    /// <summary>
    /// Checks if the player hit the ground.
    /// </summary>
    /// <param name="collision">Data regarding a collision.</param>
    private void checkCollision(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        Vector2 contactPoint = collision.GetContact(0).point;
        if (obj.CompareTag("Platform") && contactPoint.y < transform.position.y) //Second statement makes sure it's under the player.
        {
            onGround = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        checkCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (! onGround)
            checkCollision(collision);
    }
    
    /// <summary>
    /// Checks if the player left the ground.
    /// </summary>
    /// <param name="collision">Data regarding a collision.</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Platform"))
        {
            onGround = false;
        }
    }
}
