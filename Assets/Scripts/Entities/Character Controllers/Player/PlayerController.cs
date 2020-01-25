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
    private bool jump;
    private int numGround;

    public GameObject body;

    /// <summary>
    /// Prepares the main character for the game.
    /// </summary>
    private void Start()
    {
        jump = false;
        rigid = GetComponent<Rigidbody2D>();
        numGround = 0;
    }

    /// <summary>
    /// Moves the main character based on the player's input.
    /// </summary>
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 mirrorScale = body.transform.localScale;
        if (horizontal > 0)
        {
            mirrorScale.x = Mathf.Abs(mirrorScale.x);
        }
        else if (horizontal < 0)
        {
            mirrorScale.x = -Mathf.Abs(mirrorScale.x);
        }
        body.transform.localScale = mirrorScale;


        float vertical = 0;
        if (onGround) //Check for ground.
        {
            vertical = Input.GetAxis("Vertical");
            if(vertical < 0.85 && vertical > 0)
            {
                vertical = 0.85f;
            }
            if (vertical > 0)
            {
                onGround = false;
                jump = true;
                if(rigid.velocity.y > 0)
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, 0);
                }
            }
        }
        else
        {
            horizontal /= 5;
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

        //Point the body in the direction it's going.
        float currentSpeed = Mathf.Abs(rigid.velocity.x);

        if (currentSpeed > maxSpeedX)
        {
            rigid.velocity = new Vector2(rigid.velocity.x * maxSpeedX / currentSpeed, rigid.velocity.y);
        }
        currentSpeed = rigid.velocity.y;
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
        bool g = onGround;
        if (obj.CompareTag("Platform"))
        {
            for (int i = 0; i < collision.contactCount; ++i)
            {
                Vector2 contactPoint = collision.GetContact(i).point;
                if (contactPoint.y <= (transform.position.y - (GetComponent<BoxCollider2D>().size.y / 2))) //Second statement makes sure it's under the player.
                {
                    onGround = true;
                }
                else
                {
                    onGround = g;
                    return;
                }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        checkCollision(collision);
        if (collision.gameObject.CompareTag("Platform"))
        {
            ++numGround;
            Debug.Log("Entered " + numGround);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (! onGround && !jump)
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
            --numGround;
            Debug.Log("Left " + numGround);
            if (numGround <= 0)
            {
                onGround = false;
                jump = false;
            }
        }
    }
}
