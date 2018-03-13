using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections;

[System.Serializable]
public static class HighScore2
{
    public static int score;
    static public bool show;
    public static string sceneName;
    public static string resetscene;
    public static int[] scores = new int[10];
    public static string[] names = new string[10];

    public static string playerName;

    public static void befSort()
    {
        return;
        sceneName = "Level2";
        sort();
        score = 0;
        playerName = "";
        sceneName = "";
    }

    public static void sort()
    {
        return;
        //Loop through the capacity, (This is what data we already have)

        //Loop through the remaining and add new data
        Debug.Log(sceneName);
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(names[i] + scores[i]);
        }
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
        tempS.CopyTo(scores, 0);
        tempN.CopyTo(names, 0);
        //scores = tempS;
        //names = tempN;

        for (int i = 0; i < 10; i++)
        {
            Debug.Log(sceneName + ":" + names[i] + scores[i]);
        }

        save();
    }
    public static void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //scores
        FileStream file = File.Create(Application.persistentDataPath + "/" + sceneName + "Scores.gd");
        bf.Serialize(file, HighScore.scores);
        file.Close();

        //names
        FileStream file2 = File.Create(Application.persistentDataPath + "/" + sceneName + "Names.gd");
        bf.Serialize(file2, HighScore.names);
        file2.Close();

        for (int i = 0; i < 10; i++)
        {
            Debug.Log(names[i] + scores[i]);
        }
    }
    public static void load()
    {
        BinaryFormatter bf = new BinaryFormatter();
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

        for (int i = 0; i < 10; i++)
        {
            Debug.Log("ONLOAD_" + sceneName + ":" + names[i] + scores[i]);
        }
    }
    public static void clear()
    {
        HighScore.sceneName = "ProBuilderTest";
        for (int i = 0; i < 10; i++)
        {
            scores[i] = 0;
            names[i] = "";
        }
        save();

        HighScore.sceneName = "Level2";
        for (int i = 0; i < 10; i++)
        {
            scores[i] = 0;
            names[i] = "";
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
