using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingHazardController : HazardController
{
    /// <summary>
    /// Instantiates the <c>Moving Hazard</c> (if necessary).
    /// </summary>
    private void Start()
    {
        OnStart();
    }
    /// <summary>
    /// Moves the <c>Moving Hazard</c> every frame.
    /// </summary>
    private void Update()
    {
        transform.Translate(GetMoveDirection() * Time.deltaTime);
        OnUpdate();
    }
    /// <summary>
    /// Does something at the beginning of the game.
    /// </summary>
    public abstract void OnStart();
    /// <summary>
    /// Does something at every frame.
    /// </summary>
    public abstract void OnUpdate();
}
