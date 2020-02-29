using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class requires that it be in a game object with children.
/// Each child of the game object's children will be considered a hitpoint.
/// </summary>
public abstract class HealthPoints : MonoBehaviour
{
    /// <summary>
    /// The time between hits when an entity is invisible.
    /// </summary>
    public float invincTime;
    /// <summary>
    /// The timer that counts how long the entity has been invincible for.
    /// The timer counts down from invincTime between hits.
    /// </summary>
    private float invincTimer;
    /// <summary>
    /// The visual hitpoints.
    /// </summary>
    public GameObject[] hitPoints;
    /// <summary>
    /// The index of the last "available" / "on" hitpoint.
    /// </summary>
    public int divider;
    public float soundTime;
    private float soundTimer;
    public int startDamage;
    //public AudioSource sFXHealth;

    // Start is called before the first frame update
    public void Start()
    {
        hitPoints = new GameObject[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            hitPoints[i] = child.gameObject;
            i++;
        }
        divider = hitPoints.Length - 1 - startDamage;
        for(int a = divider +1; a < hitPoints.Length; ++a)
        {
            hitPoints[a].SetActive(false);
        }
        invincTimer = invincTime;
    }

    /// <summary>
    /// Decrements the invincibility timer every frame.
    /// </summary>
    void Update()
    {
        if (divider == 0)
        {

        }
        if (invincTimer > 0)
        {
            invincTimer -= Time.deltaTime;
            if (invincTimer < 0)
            {
                invincTimer = 0;
            }
        }
        if (soundTimer == 0)
        {
            soundTimer = soundTime;

            soundTimer -= Time.deltaTime;
            if (soundTimer < 0)
            {
                soundTimer = 0;
            }
        }
    }

    /// <summary>
    /// Removes one hit point. When there are no more hitpoints, NoHealth is called.
    /// </summary>
    public void RemoveHitPoint()
    {
        hitPoints[divider].SetActive(false);
        
        if (divider == 0)
        {
            NoHealth();
        }

        divider--;
    }

    /// <summary>
    /// Adds one hit point.
    /// </summary>
    /// <returns>Returns whether there was space to add hit points or not.</returns>
    private bool AddHitPoint()
    {
        if (divider != hitPoints.Length - 1)
        {
            divider++;
            hitPoints[divider].SetActive(true);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds multiple hit points.
    /// </summary>
    /// <param name="n">The number of hit points to add.</param>
    public void AddHitPoints(int n)
    {
        for (int i = 0; i < n; i++)
        {
            AddHitPoint();
        }
    }

    /// <summary>
    /// Removes a hit point if the invincibility timer is out.
    /// </summary>
    /// <returns>Whether a hit point was removed or not.</returns>
    public bool Hit()
    {
        if (invincTimer > 0)
        {
            return false;
        }
        else
        {
            invincTimer = invincTime;

            RemoveHitPoint();

            return true;
        }
    }

    /// <summary>
    /// Does something when the entity runs out of hit points.
    /// For example, when the player runs out of hit points,
    /// the game is reset.
    /// </summary>
    public abstract void NoHealth();
}
