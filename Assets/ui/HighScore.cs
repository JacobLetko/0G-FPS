using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{ 

    int score;
    public bool show;

    public string playerName;

    void table()
    {
        for (int i = 0; i < 10; i++)
        {
            if(score > PlayerPrefs.GetInt("score"+i))
            {

            }
        }
    }

    public void resetScore()
    {
        score = 0;
    }

    public void addPoints(int points)
    {
        score += points;
    }
}
