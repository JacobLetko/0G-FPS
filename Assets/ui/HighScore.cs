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

    public static List<int> scores = new List<int>();
    public static List<string> names = new List<string>();
    
    public static string playerName;

    public static void sort()
    {
        int temp = score;
        string tempN = playerName;
        string tempN2;
        for(int i = 0; i < 10; i++)
        {
            if (scores[i] < temp)
            {
                temp = temp + scores[i];
                scores[i] = temp - scores[i];
                temp = temp - scores[i];

                tempN2 = names[i];
                names[i] = tempN;
            }
        }
    }

    public static void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        
        //scores
        FileStream file = File.Create(Application.persistentDataPath + "/Scores.gd");
        bf.Serialize(file, HighScore.scores);
        file.Close();

        //names
        FileStream file2 = File.Create(Application.persistentDataPath + "/Names.gd");
        bf.Serialize(file, HighScore.names);
        file.Close();
    }
    public static void load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        
        //scores
        if (File.Exists(Application.persistentDataPath + "/Scores.gd"))
        {    
            FileStream file = File.Open(Application.persistentDataPath + "/Scores.gd", FileMode.Open);
            HighScore.scores = (List<int>)bf.Deserialize(file);
            file.Close();
        }
        if (File.Exists(Application.persistentDataPath + "/Names.gd"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/Names.gd", FileMode.Open);
            HighScore.names = (List<string>)bf.Deserialize(file);
            file.Close();
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
