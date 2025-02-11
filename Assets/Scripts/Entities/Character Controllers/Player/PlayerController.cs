﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool releaseJump;
    private float prevVert;
    public bool canGlide;
    private bool horizontalDash;
    public float dashInterval = 0.8f;

    public GameObject body;

    public Animator bodyAnim;

    public AudioSource sFXPlayer;

    public string idleAnimationName = "birdIdleAnimation";
    public string jumpAnimationName = "birdJumpAnimation";
    public string dashAnimationName = "lizardDashAnimation";
    public string glideAnimationName = "flyingSquirrelGlide";

    private bool hasGlided;

    public float glideCoeff = 0.005f;

    public LizardTailController tail;

    public enum CharacterType
    {
        TESTING = 0,
        BIRD = 1,
        LIZARD = 2,
        SQUIREL = 3
    }

    /// <summary>
    /// Prepares the main character for the game.
    /// </summary>
    /// 
    private void Start()
    {
        releaseJump = true;
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
            //Sets the player using data from file.
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
                canGlide = data.canGlide;
            }
        }
        accelCoeff.x *= 60;
        glideCoeff *= 60;
    }

    /// <summary>
    /// Moves the main character based on the player's input.
    /// </summary>
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0 && onGround) //Play sounds and animation if player's on ground
        {
            bodyAnim.SetTrigger("Walking");
            Vector3 mirrorScale = body.transform.localScale;
            if(!sFXPlayer.isPlaying)
            {
                sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/LeafWalk");
                sFXPlayer.Play();
            }
            if (horizontal > 0)
            {
                mirrorScale.x = Mathf.Abs(mirrorScale.x);
                if (tail != null)
                {
                    tail.SetFlip(false);
                }
            }
            else if (horizontal < 0)
            {
                mirrorScale.x = -Mathf.Abs(mirrorScale.x);
                if (tail != null)
                {
                    tail.SetFlip(true);
                }
            }
            body.transform.localScale = mirrorScale;
        }
        else if (onGround)
        {
            bodyAnim.Play(idleAnimationName);
        }

        float vertical = 0;//Vertical is zero until we know you jump.
        float v = Input.GetAxis("Vertical");
        bool hitJump = false;//hitJump added to prevent wasting double jump.
        if (v < 0.85 && v >= 0.1)
        {
            v = 0.85f;
            if (releaseJump)//Only jump if you've released jump.
            {
                hitJump = true;
                releaseJump = false;
            }
        }
        if(v < 0.1 && v >= 0)//Discount tiny presses.
        {
            v = 0;
        }
        if(v <= 0 || v < prevVert - 0.005)//Detecting released jump.
        {
            releaseJump = true;
            if (numJumps < maxJumps)
            {
                airJump = true;
            }
        }
        if (onGround && v < 0 && crouchCount <= 0 && canCrouch)//Crouch when pressing down.
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
        if(!onGround)//Slow acceleration while in the air.
        {
            horizontal /= 5;
        }
        if(crouchCount > 0)
        {
            --crouchCount;
        }

        if ((onGround || airJump) && v > 0 && hitJump)//Jump if player is capable.
        {
            bodyAnim.Play(jumpAnimationName);
            ++numJumps;
            onGround = false;
            jump = true;
            airJump = false;
            vertical = v;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            if (airJump == true)//Play sound effect depending on if the player is jumping off ground or in the air.
            {
                sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/JumpSwoop");
                sFXPlayer.Play();
            }
            else
            {
                sFXPlayer.clip = Resources.Load<AudioClip>("Sounds/WingSwoosh");
                sFXPlayer.Play();
            }
        }
        if (numDash < maxDash && Input.GetMouseButtonDown(0))//Dash when mouse is pressed
        {
            if (horizontal != 0 || v != 0)//Don't waste dash if neither horizontal or vertical button is being pressed.
            {
                ++numDash;
                if (horizontal != 0)
                {
                    horizontal = dashSpeedX * horizontal / Mathf.Abs(horizontal);//Dash speed is independant of how much the direction is being pressed.
                }
                vertical = v * dashSpeedY;
                horizontalDash = true;
                bodyAnim.Play(dashAnimationName);
                rigid.velocity = new Vector2(rigid.velocity.x, 0);//Reset vertical velocity before dashing.
            }
        }
        prevVert = v;
        

        if (canGlide && rigid.velocity.y < 0  && Mathf.Abs(rigid.velocity.x) > 0 && vertical == 0 && v > 0)//Enable gliding if the player is moving down, and horizontally and they're pressing up.
        {
            vertical = v * rigid.velocity.y * glideCoeff * (Physics.gravity.y * rigid.mass) * Time.deltaTime;
            if (! hasGlided)
            {
                bodyAnim.Play(glideAnimationName);
                hasGlided = true;
            }
        }
        else
        {
            hasGlided = false;
        }

        Move(horizontal, vertical);
        if (onGround)
        {
            numJumps = 0;
            numDash = 0;
        }
        //Set the static data file to update the displays.
        Data.playerPos = new Vector2(transform.position.x, transform.position.y);
        Data.playerJumps = maxJumps - numJumps;
        Data.playerDashes = maxDash - numDash;
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
            //Apply the slow from crouching.
            horizontal *= crouchSlow;
        }
        
        float xSpeed = rigid.velocity.x;
        if(xSpeed > maxSpeedX)
        {
            xSpeed = maxSpeedX;
        }
        else if(xSpeed < maxSpeedX * -1)
        {
            xSpeed = maxSpeedX * -1;
        }
        if (xSpeed != 0 && horizontal / xSpeed > 0 && !horizontalDash)
        {
            //Set horizontal so you can't exceed max speed x by your own movement.
            horizontal *= Mathf.Pow(maxSpeedX - Mathf.Abs(xSpeed), 0.1f);
        }
        horizontalDash = false;

        float ySpeed = rigid.velocity.y;
        if (ySpeed > maxSpeedY)
        {
            ySpeed = maxSpeedY;
        }
        if (ySpeed != 0 && vertical / xSpeed > 0)
        {
            vertical *= Mathf.Pow(maxSpeedY - Mathf.Abs(ySpeed), 0.1f);
        }
        rigid.AddForce(new Vector2(horizontal * accelCoeff.x * Time.deltaTime, vertical * accelCoeff.y));

        /*Old movement method
        float currentSpeed = rigid.velocity.x;
        float maxX = maxSpeedX;
        if (crouch)
        {
            maxX *= crouchSlow;
        }
        if (currentSpeed > maxX && horizontal > 0)
        {
            rigid.velocity = new Vector2(maxX, rigid.velocity.y);
        }
        else if(currentSpeed < maxX * -1 && horizontal < 0)
        {
            rigid.velocity = new Vector2(maxX * -1, rigid.velocity.y);
        }
        currentSpeed = rigid.velocity.y;
        if (currentSpeed > maxSpeedY)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, maxSpeedY);
        }*/
    }

    private void Dash()
    {

    }

    /// <summary>
    /// Checks if the player hit the ground.
    /// </summary>
    /// <param name="collision">Data regarding a collision.</param>
    private void checkCollision(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        bool g = onGround;
        if (obj.CompareTag("Platform") || obj.CompareTag("Hazard"))//Only land if what you stand on is a ground object.
        {
            for (int i = 0; i < collision.contactCount; ++i)//Ensure the platform is below you.
            {
                Vector2 contactPoint = collision.GetContact(i).point;
                if (contactPoint.y <= (transform.position.y - ((GetComponent<CapsuleCollider2D>().size.y / 2 - GetComponent<CapsuleCollider2D>().size.x / 2))) && collision.GetContact(i).normal.y > 0) //Second statement makes sure it's under the player.
                {
                    onGround = true;
                    bodyAnim.SetBool("OnGround", true);
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
        else if(collision.gameObject.CompareTag("Hazard"))
        {
            ++numGround;
            GetComponentInChildren<HitArea>().Damage();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponentInChildren<HitArea>().Damage();
        }
        else if (collision.gameObject.CompareTag("Health"))
        {
            GetComponentInChildren<HitArea>().Heal(collision.gameObject.GetComponent<HealthPack>().heal);
            Destroy(collision.gameObject);
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
        if (obj.CompareTag("Platform") || obj.CompareTag("Hazard"))
        {
            --numGround;
            if (numGround <= 0)
            {
                onGround = false;
                bodyAnim.SetBool("OnGround", false);
                jump = false;
            }
        }
    }

    public bool OnGround()
    {
        return (onGround);
    }

    public bool AddDash()
    {
        if(numDash > 0)
        {
            --numDash;
            return (true);
        }
        return (false);
    }
}
