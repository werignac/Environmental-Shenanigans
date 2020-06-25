using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Data.rooms = 15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer(int player)
    {
        Data.player = player;
    }

    public void StartGame(int level)
    {
        Data.level = level / 4;
        Data.startRoom = level + Data.player - 1;
        SceneManager.LoadScene("MainGame");
    }
}
