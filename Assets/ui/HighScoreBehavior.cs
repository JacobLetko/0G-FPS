﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class HighScoreObject
{
    public string name;
    public int score;
}
[System.Serializable]
public class HighScoreTable
{
    public string forLevel;
    [SerializeField]
    public List<HighScoreObject> scoreTable;
   // public HighScoreObject[] tmparr { get; set; }

}

/// <summary>
/// Jons stuff works dont delete any of it unless you know what you doing
/// </summary>
public class HighScoreBehavior : MonoBehaviour {

    public Text level1scores;
    public Text level2scores;
    public Text scoretext;
    //Jons Crap
    public HighScoreTable level2Tbl = new HighScoreTable();
    public HighScoreTable probuildertestTable = new HighScoreTable();
    public bool CurrentPlayerScoreAdded = false;


    public void display()
    {
        scoretext.text = "Score: " + HighScore.score;
    }

    public void table()
    {
        if (!CurrentPlayerScoreAdded)
        {
            if (HighScore.sceneName == "Level2")
            {
                AppendNewScore(HighScore.playerName, HighScore.score, level2Tbl);
                SaveTable(level2Tbl, HighScore.sceneName);
            }
            if (HighScore.sceneName == "ProBuilderTest")
            {
                AppendNewScore(HighScore.playerName, HighScore.score, probuildertestTable);
                SaveTable(probuildertestTable, HighScore.sceneName);
            }
        }

        level1scores.text = GetDataIntoText(probuildertestTable);
        level2scores.text = GetDataIntoText(level2Tbl);
    }

    public void addName(Text name)
    {
        HighScore.playerName = name.text.ToString();
        if(HighScore.playerName == "")
        {
            HighScore.playerName = "Player";
        }
    }
    public void ResetScores()
    {
        //Jons Stuff
        level2Tbl.scoreTable = new List<HighScoreObject>();
        probuildertestTable.scoreTable = new List<HighScoreObject>();

        SaveTable(level2Tbl, "Level2");
        SaveTable(probuildertestTable, "ProBuilderTest");

        table();
    }

    public void Awake()
    {
        //Re-initialize the data containers
        level2Tbl.scoreTable = new List<HighScoreObject>();
        probuildertestTable.scoreTable = new List<HighScoreObject>();

        //for level is useful if we had a totally dynamic list of tables
        level2Tbl.forLevel = "Level2";
        //probuildertestTable.forLevel = "Probuilder";

        //Load in the data
        level2Tbl = LoadTable("Level2");
        probuildertestTable = LoadTable("ProBuilderTest");
        
        display();
    }


    /// <summary>
    /// Pass a data HST into this to get a 'formatted' string of scores back
    /// </summary>
    /// <param name="hst"></param>
    /// <returns></returns>
    string GetDataIntoText(HighScoreTable hst)
    {
        string rtn = "";
        for (int i = 0; i < hst.scoreTable.Count; i++)
        {
            if (i < 10)
            {
                rtn += (i + 1) + ".   " + hst.scoreTable[i].name + "   " + hst.scoreTable[i].score + "\n";
            }
        }

        return rtn;
    }


    /// <summary>
    /// Appends the score
    /// </summary>
    /// <param name="pName"></param>
    /// <param name="pScore"></param>
    /// <param name="hst"></param>
    void AppendNewScore(string pName,int pScore, HighScoreTable hst)
    {
        HighScoreObject hso = new HighScoreObject();
        hso.name = pName;
        hso.score = pScore;
        hst.scoreTable.Add(hso);
        hst.scoreTable = hst.scoreTable.OrderByDescending(o => o.score).ToList<HighScoreObject>();
        CurrentPlayerScoreAdded = true;
    }


    /// <summary>
    /// Loads a table from the given filename
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
   HighScoreTable LoadTable(string filename)
    {
        HighScoreTable rtn = new HighScoreTable();

        if (PlayerPrefs.HasKey(filename))
        {
            string tmpstr = PlayerPrefs.GetString(filename);
            rtn = JsonUtility.FromJson<HighScoreTable>(tmpstr);
        }
        else
        {
            rtn.scoreTable = new List<HighScoreObject>();
        }
        return rtn; 
    }

    /// <summary>
    /// Saves the Highscore table passed in
    /// </summary>
    /// <param name=""></param>
    void SaveTable(HighScoreTable hst,string fname)
    {
      // hst.tmparr= hst.scoreTable.ToArray<HighScoreObject>();
        string tmp = JsonUtility.ToJson(hst);
        PlayerPrefs.SetString(fname, tmp);
        PlayerPrefs.Save();
    }

}
