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

    public override void Start()
    {
        base.Start();
        float xPos = -Camera.main.orthographicSize * Screen.width / Screen.height;
        for (int i = 0; i < hitPoints.Length; ++i)
        {
            xPos += hitPoints[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;
            hitPoints[i].transform.position = new Vector3(xPos, hitPoints[i].transform.position.y);
            xPos += hitPoints[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }
    }
}
