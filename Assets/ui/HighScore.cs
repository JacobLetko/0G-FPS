using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections;

[System.Serializable]
public static class HighScore
{
    public static int score;
    static public bool show;
    public static string sceneName;
    public static string resetscene;
    
    public static string playerName;

    public static void resetScore()
    {
        score = 0;
    }

    public static void addPoints(int points)
    {
        score += points;
    }
}
