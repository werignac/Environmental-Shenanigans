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
    public bool walkSoundCooldown;
    private bool jump;
    private int numGround;
    private int numJumps;
    public int maxJumps;
    private bool airJump;
    private bool crouch;
    private int crouchCount;
    public float crouchSlow;
    public CharacterType character;
    public bool canCrouch;
    public int maxDash;
    private int numDash;
    public float dashSpeedX;
    public float dashSpeedY;
    public float mass;

    public GameObject body;

    public Animator bodyAnim;

    public AudioSource sFXPlayer;

    public enum CharacterType
    {
        TESTING = 0,
        BIRD = 1
    }

    /// <summary>
    /// Prepares the main character for the game.
    /// </summary>
    /// 
    private void Start()
    {
        jump = false;
        rigid = GetComponent<Rigidbody2D>();
        numGround = 0;
        airJump = false;
        numJumps = 0;
        crouch = false;
        crouchCount = 0;
        numDash = 0;
        if (character >= 0)
        {
            PlayerData data = Data.GetPlayerData((int) character);
            if (data != null)
            {
                accelCoeff = data.accelCoeff;
                maxSpeedX = data.maxSpeedX;
                maxSpeedY = data.maxSpeedY;
                maxJumps = data.maxJumps;
                crouchSlow = data.crouchSlow;
                canCrouch = data.canCrouch;
                maxDash = data.maxDash;
                dashSpeedX = data.dashSpeedX;
                dashSpeedY = data.dashSpeedY;
                mass = data.mass;
            }
        }
    }

    /// <summary>
    /// Moves the main character based on the player's input.
    /// </summary>
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0 && onGround)
        {
            bodyAnim.SetTrigger("Walking");
            Vector3 mirrorScale = body.transform.localScale;
            if(!sFXPlayer.isPlaying)
            {
                sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/JumpSwoop");
                sFXPlayer.Play();
            }
            if (horizontal > 0)
            {
                mirrorScale.x = Mathf.Abs(mirrorScale.x);
            }
            else if (horizontal < 0)
            {
                mirrorScale.x = -Mathf.Abs(mirrorScale.x);
            }
            body.transform.localScale = mirrorScale;
        }
        else
        {
            bodyAnim.Play("birdIdleAnimation");
        }

        //hitJump added to prevent wasting double jump.
        bool hitJump = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        float vertical = 0;
        float v = Input.GetAxis("Vertical");
        if (v < 0.85 && v > 0 && hitJump)
        {
            v = 0.85f;
        }
        if(v == 0)
        {
            if (numJumps < maxJumps)
            {
                airJump = true;
            }
        }
        if ((onGround || airJump) && v > 0 && hitJump)
        {
            ++numJumps;
            onGround = false;
            jump = true;
            airJump = false;
            vertical = v;
            if (rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
            }
            sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/JumpSwoop");
            sFXPlayer.Play();
        }
        if (onGround && v < 0 && crouchCount <= 0 && canCrouch)
        {
            if (crouch)
            {
                crouch = false;
                GetComponent<CapsuleCollider2D>().size = new Vector2(GetComponent<CapsuleCollider2D>().size.x, GetComponent<CapsuleCollider2D>().size.y * 2);
            }
            else
            {
                crouch = true;
                GetComponent<CapsuleCollider2D>().size = new Vector2(GetComponent<CapsuleCollider2D>().size.x, GetComponent<CapsuleCollider2D>().size.y / 2);
            }
            crouchCount = (int)Data.frameRate / 2;
        }
        if(!onGround)
        {
            horizontal /= 5;
        }
        if(crouchCount > 0)
        {
            --crouchCount;
        }
        if(numDash < maxDash && Input.GetKeyDown(KeyCode.Space))
        {
            ++numDash;
            horizontal *= dashSpeedX;
            vertical = v * dashSpeedY;
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
        if (crouch)
        {
            horizontal *= crouchSlow;
        }
        rigid.AddForce(new Vector2(horizontal * accelCoeff.x, vertical * accelCoeff.y));

        //Point the body in the direction it's going.
        float currentSpeed = Mathf.Abs(rigid.velocity.x);
        float maxX = maxSpeedX;
        if (crouch)
        {
            maxX *= crouchSlow;
        }
        if (currentSpeed > maxX)
        {
            rigid.velocity = new Vector2(rigid.velocity.x * maxX / currentSpeed, rigid.velocity.y);
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
                if (contactPoint.y <= (transform.position.y - ((GetComponent<CapsuleCollider2D>().size.y / 2 - GetComponent<CapsuleCollider2D>().size.x / 2)))) //Second statement makes sure it's under the player.
                {
                    onGround = true;
                    numJumps = 0;
                    numDash = 0;
                }
                else
                {
                    onGround = g;
                    if (onGround)
                    {
                        numJumps = 0;
                        numDash = 0;
                    }
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
            if (numGround <= 0)
            {
                onGround = false;
                jump = false;
            }
        }
    }
}
