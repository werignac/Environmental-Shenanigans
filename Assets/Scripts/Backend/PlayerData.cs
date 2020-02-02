using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    string name;
    /// <summary>
    /// The acceleration of the main character for horizontal and vertical movement.
    /// </summary>
    public Vector2 accelCoeff;
    /// <summary>
    /// The maximum horizontal velocity the main character can reach.
    /// </summary>
    public float maxSpeedX;
    /// <summary>
    /// The maximum vertical velocity the main character can reach.
    /// </summary>
    public float maxSpeedY;
    public int maxJumps;
    public float crouchSlow;
    public bool canCrouch;
    public int maxDash;
    public float dashSpeedX;
    public float dashSpeedY;

    public PlayerData(string n, float aX, float aY, float mX, float mY, int mJ, float cS, bool cC, int mD, float dSX, float dSY)
    {
        name = n;
        accelCoeff = new Vector2(aX, aY);
        maxSpeedX = mX;
        maxSpeedY = mY;
        maxJumps = mJ;
        crouchSlow = cS;
        canCrouch = cC;
        maxDash = mD;
        dashSpeedX = dSX;
        dashSpeedY = dSY;
    }
}
