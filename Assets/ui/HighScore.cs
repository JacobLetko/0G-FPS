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


    public static int[] scores = new int[10];
    public static string[] names = new string[10];
    
    public static string playerName;



    public static void sort()
    {
        //Loop through the capacity, (This is what data we already have)

        //Loop through the remaining and add new data

        load();

        int[] tempS = new int[10];
        string[] tempN = new string[10];
        bool scoreIn = false;

        for (int i = 0; i < 10; i++)
        {
            if (scores[i] < score && scoreIn == false)
            {
                tempS[i] = score;
                tempN[i] = playerName;
                if (i != 9)
                {
                    tempS[i + 1] = scores[i];
                    tempN[i + 1] = names[i];
                }
                scoreIn = true;
            }
            else if (scores[i] < score && scoreIn == true)
            {
                if (i != 9)
                {
                    tempS[i + 1] = scores[i];
                    tempN[i + 1] = names[i];
                }
            }
            else
            {
                tempS[i] = scores[i];
                tempN[i] = names[i];
            }
        }
        scores = tempS;
        names = tempN;
    }

    public static void save()
    {
        sort();
        BinaryFormatter bf = new BinaryFormatter();
        
        //scores
        FileStream file = File.Create(Application.persistentDataPath + "/Scores.gd");
        bf.Serialize(file, HighScore.scores);
        file.Close();

        //names
        FileStream file2 = File.Create(Application.persistentDataPath + "/Names.gd");
        bf.Serialize(file2, HighScore.names);
        file2.Close();
    }
    public static void load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        
        //scores
        if (File.Exists(Application.persistentDataPath + "/Scores.gd"))
        {    
            FileStream file = File.Open(Application.persistentDataPath + "/Scores.gd", FileMode.Open);
            HighScore.scores = (int[])bf.Deserialize(file);
            file.Close();
        }
        if (File.Exists(Application.persistentDataPath + "/Names.gd"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/Names.gd", FileMode.Open);
            HighScore.names = (string[])bf.Deserialize(file);
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
