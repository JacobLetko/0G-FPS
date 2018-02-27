using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighScore
{
    static int score;
    static public bool show;

    public static string playerName;

    static void table()
    {
        for (int i = 0; i < 10; i++)
        {
            if(score > PlayerPrefs.GetInt("score"+i))
            {

            }
        }
    }

    public static void resetScore()
    {
        score = 0;
    }

    public static void addPoints(int points)
    {
        score += points;
    }
}
