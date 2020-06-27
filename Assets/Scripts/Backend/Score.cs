using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    public int deaths;
    public float time;
    public Score(int d, float t)
    {
        deaths = d;
        time = t;
    }

    public string getText()
    {
        string t = "";
        if (time >= 6000)
        {
            t = "99:59";
        }
        else
        {
            t += Mathf.FloorToInt(time / 60) + ":" + Mathf.FloorToInt(time % 60);
        }
        return (t + "\nX: " + deaths);
    }
}
