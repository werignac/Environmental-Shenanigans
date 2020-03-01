using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseControl : MonoBehaviour
{
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 300;
    }

    // Update is called once per frame
    void Update()
    {
        --count;
        if(count <= 0)
        {
            SceneManager.LoadScene("MainMenu");//Return to main menu after 300 frames.
        }
    }
}
