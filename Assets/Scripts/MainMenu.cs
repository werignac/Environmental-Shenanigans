using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Data.rooms = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int player)
    {
        Data.player = player;
        SceneManager.LoadScene("MainGame");
    }
}
