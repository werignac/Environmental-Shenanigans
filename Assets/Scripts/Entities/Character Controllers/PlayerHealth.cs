using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The hit point controller for the player.
/// </summary>
public class PlayerHealth : HealthPoints
{
    /// <summary>
    /// Loads the first scene when the player dies.
    /// </summary>
    public override void NoHealth()
    {
        SceneManager.LoadScene(2);
    }
}
