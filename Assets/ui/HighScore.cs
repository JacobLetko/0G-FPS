using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighScore
{ 

    static int score;

    public static void resetScore()
    {
        score = 0;
    }

    public static void addPoints(int points)
    {
        score += points;
    }
}
