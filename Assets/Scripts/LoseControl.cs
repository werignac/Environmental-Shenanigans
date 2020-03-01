using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseControl : MonoBehaviour
{
    public float closeTime;

    // Update is called once per frame
    void Update()
    {
        closeTime -= Time.deltaTime;
        if(closeTime <= 0)
        {
            SceneManager.LoadScene("MainMenu");//Return to main menu after closeTime seconds.
        }
    }
}
