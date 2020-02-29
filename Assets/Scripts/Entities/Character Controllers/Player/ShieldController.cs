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
    private float mass;
    private bool first;

    public float reflectionDelay;
    private float timer;
    public AudioSource sFXPlayer;
    public SpriteRenderer headColor;
    private int projectileCount;
    private PlayerController pC;
    /// <summary>
    /// Stores the initialScale;
    /// </summary>
    private void Start()
    {
        initialScale = transform.localScale;
        anim = GetComponentInChildren<Animator>();
        first = true;
        timer = 0;
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
        if (first)
        {
            first = false;
            pC = player.gameObject.GetComponent<PlayerController>();
            mass = pC.mass;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                headColor.color = new Color(255,255,255);
                
                if (headColor.transform.childCount > 0)
                {
                    SpriteRenderer child = headColor.transform.GetChild(0).GetComponent<SpriteRenderer>();

                    while (child.transform.childCount > 0)
                    {
                        child.color = new Color(255, 255, 255);

                        child = child.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    }
                }
            }
        }
        if (pC.OnGround())
        {
            projectileCount = 0;
        }
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
        if (encounter.CompareTag("Hazard") || encounter.CompareTag("Projectile") || encounter.CompareTag("Enemy") || encounter.CompareTag("Explosion"))
        {
            if (timer == 0)
            {
                sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/Bounce");
                sFXPlayer.Play();
                if (anim != null)
                {
                    anim.SetTrigger("Hit");
                }
                HazardController hazard = encounter.GetComponent<HazardController>();
                if (hazard == null)
                {
                    hazard = encounter.GetComponentInParent<HazardController>();
                }
                if (hazard == null)
                {
                    hazard = encounter.GetComponentInChildren<HazardController>();
                }
                Vector2 speed = new Vector2();
                float hMass = 1;
                if (hazard != null)
                {
                    speed = hazard.GetMoveDirection();
                    if (encounter.CompareTag("Explosion"))
                    {
                        speed = encounter.transform.position - player.transform.position;
                        speed.Normalize();
                        speed *= encounter.GetComponent<ExplosionController>().speed;
                    }
                    hMass = hazard.mass;
                }
                Impact(speed, hMass);
                if (hazard != null)
                {
                    hazard.OnShieldCollision(gameObject);
                }

                timer = reflectionDelay;
                projectileCount++;
                if(projectileCount >= 5)
                {
                    if (pC.AddDash())
                    {
                        projectileCount = 0;
                    }
                }

                headColor.color = new Color(0, 0, 255);
                if (headColor.transform.childCount > 0)
                {
                    SpriteRenderer child = headColor.transform.GetChild(0).GetComponent<SpriteRenderer>();

                    while (child.transform.childCount > 0)
                    {
                        child.color = new Color(0, 0, 255);

                        child = child.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Sends the player flying in the opposite direction with an additional velocity.
    /// </summary>
    /// <param name="velocity">The additional velocity.</param>
    public void Impact(Vector2 velocity, float projMass)
    {
        Vector2 angle = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z));
        player.velocity = angle * (-1 * player.velocity.magnitude);
        player.AddForce(angle * (-2 * projMass * velocity.magnitude));
        player.AddForce(projMass * velocity);
    }

}
