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
        transform.position = transform.position + (Vector3) (GetMoveDirection() * Time.deltaTime);
        OnUpdate();
        DebugDisplay();
    }
    /// <summary>
    /// Does something at the beginning of the game.
    /// </summary>
    public abstract void OnStart();
    /// <summary>
    /// Does something at every frame.
    /// </summary>
    public abstract void OnUpdate();
    /// <summary>
    /// Display's the direction the hazards are moving in Debug Mode.
    /// </summary>
    private void DebugDisplay()
    {
#if UNITY_EDITOR
        Debug.DrawLine(transform.position, transform.position + ((Vector3)GetMoveDirection()),Color.red);
#endif
    }
}
