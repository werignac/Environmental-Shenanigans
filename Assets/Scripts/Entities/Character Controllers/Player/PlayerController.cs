using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    public Vector2 accelCoeff;
    private bool onGround;
    public float maxSpeed;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        float vertical = 0;
        if (onGround) //Check for ground.
        {
            vertical = Input.GetAxis("Vertical");
            onGround = false;
        }

        rigid.AddForce(new Vector2(horizontal * accelCoeff.x, vertical * accelCoeff.y));

        float currentSpeed = rigid.velocity.magnitude;
        if (currentSpeed > maxSpeed)
        {
            rigid.velocity = rigid.velocity * maxSpeed / currentSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        Vector2 contactPoint = collision.GetContact(0).point;
        if (obj.CompareTag("Platform") && contactPoint.y < transform.position.y) //Second statement makes sure it's under the player.
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Platform"))
        {
            onGround = false;
        }
    }
}
