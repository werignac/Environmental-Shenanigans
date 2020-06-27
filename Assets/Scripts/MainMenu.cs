using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text[] scoreTexts;
    // Start is called before the first frame update
    void Start()
    {
        Data.rooms = 15;
        Data.LoadScoreData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer(int player)
    {
        Data.player = player;
        for(int i = 0; i < scoreTexts.Length; ++i)
        {
            if (Data.levelScores[player][i][0] == null)
            {
                scoreTexts[i].text = "-";
            }
            else
            {
                scoreTexts[i].text = "Least Deaths\n";
                scoreTexts[i].text += Data.levelScores[player][i][0].getText();
                scoreTexts[i].text += "\nFastest Run\n" + Data.levelScores[player][i][1].getText();
            }
        }
    }

    public void StartGame(int level)
    {
        Data.level = level / 4;
        Data.startRoom = level + Data.player - 1;
        SceneManager.LoadScene("MainGame");
    }
}
