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

    public static int[] scores = new int[10];
    public static string[] names = new string[10];
    
    public static string playerName;

    public static void befSort()
    {
        if (sceneName == "ProBuilderTest")
        {
            sort();
            score = 0;
            playerName = "";
        }
        else if (sceneName == "Level2")
        {
            sort();
            score = 0;
            playerName = "";
        }
        else
        {
            sceneName = "ProBuilderTest";
            sort();
            sceneName = "Level2";
            sort();
        }
    }

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
        save();
    }
    public static void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log(sceneName + "save");
        //scores
        FileStream file = File.Create(Application.persistentDataPath + "/" + sceneName + "Scores.gd");
        bf.Serialize(file, HighScore.scores);
        file.Close();

        //names
        FileStream file2 = File.Create(Application.persistentDataPath + "/" + sceneName + "Names.gd");
        bf.Serialize(file2, HighScore.names);
        file2.Close();
    }
    public static void load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log(sceneName + "load");
        //scores
        if (File.Exists(Application.persistentDataPath + "/" + sceneName + "Scores.gd"))
        {    
            FileStream file = File.Open(Application.persistentDataPath + "/" + sceneName + "Scores.gd", FileMode.Open);
            HighScore.scores = (int[])bf.Deserialize(file);
            file.Close();
        }
        if (File.Exists(Application.persistentDataPath + "/" + sceneName + "Names.gd"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/" + sceneName + "Names.gd", FileMode.Open);
            HighScore.names = (string[])bf.Deserialize(file);
            file.Close();
        }
    }
    public static void clear()
    {
        HighScore.sceneName = "ProBuilderTest";
        for (int i = 0; i < 10; i++)
        {
            scores[i] = 0;
            names[i] = "player";
        }
        save();

        HighScore.sceneName = "Level2";
        for (int i = 0; i < 10; i++)
        {
            scores[i] = 0;
            names[i] = "player";
        }
        save();

        HighScore.sceneName = "";
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
