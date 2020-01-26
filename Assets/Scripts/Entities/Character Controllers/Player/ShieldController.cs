using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that describes the behavior of the player's shield.
/// </summary>
public class ShieldController : MonoBehaviour
{
    /// <summary>
    /// The <c>Rigidbody</c> of the main character.
    /// </summary>
    public Rigidbody2D player;
    /// <summary>
    /// The initial scale of the sheild (used for flipping).
    /// </summary>
    private Vector3 initialScale;
    /// <summary>
    /// Animator of sheild.
    /// </summary>
    private Animator anim;
    /// <summary>
    /// Stores the initialScale;
    /// </summary>
    private void Start()
    {
        initialScale = transform.localScale;
        anim = GetComponentInChildren<Animator>();
    }
    /// <summary>
    /// Rotates the shield so that it points towards a point on the screen (usually the mouse).
    /// </summary>
    /// <param name="point">The place to rotate the sheild towards.</param>
    private void RotateTowards(Vector2 point)
    {
        float xDiff = point.x - transform.position.x;
        float yDiff = point.y - transform.position.y;
        float angle = Mathf.Atan2(yDiff, xDiff)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0,angle);
        if (xDiff < 0)
        {
            transform.localScale = new Vector3(initialScale.x, -initialScale.y, initialScale.z);
        }
        else
        {
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        }
    }

    /// <summary>
    /// Updates the shield's rotation so that it points towards the player's mouse.
    /// </summary>
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateTowards(mousePos);
    }

    /// <summary>
    /// Detects if a <c>Hazard</c> comes into contact with the player's sheild.
    /// If so, the player is bounced back.
    /// If the <c>Hazard</c> has a reaction to being hit (e.g. <c>Projectiles</c> being reflected),
    /// then that rection is triggered.
    /// </summary>
    /// <param name="collider">Information about the collision that ocurred.</param>
    /// <seealso cref="HazardController"/>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject encounter = collider.gameObject;
        if (encounter.CompareTag("Hazard") || encounter.CompareTag("Projectile"))
        {
            anim.SetTrigger("Hit");
            HazardController hazard = encounter.GetComponent<HazardController>();
            Impact(hazard.GetMoveDirection());

            hazard.OnShieldCollision(gameObject);
        }
    }

    /// <summary>
    /// Sends the player flying in the opposite direction with an additional velocity.
    /// </summary>
    /// <param name="velocity">The additional velocity.</param>
    public void Impact(Vector2 velocity)
    {
        Vector2 angle = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z));
        player.velocity = angle * (-1 * (player.velocity.magnitude + velocity.magnitude));
    }

}
