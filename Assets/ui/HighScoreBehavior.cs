using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreBehavior : MonoBehaviour {

    public Text level1scores;
    public Text level2scores;
    public Text scoretext;

    public void display()
    {
        scoretext.text = "Score: " + HighScore.score;
    }

    public void table()
    {
        HighScore.sceneName = "ProBuilderTest";
        HighScore.load();
        for (int i = 0; i < HighScore.names.Length; i++)
            {
            if (i == 0)
            {
                level1scores.text = (i + 1) + ".    " + HighScore.names[i] + "   " + HighScore.scores[i] + "\n";
            }
            else
            {
                level1scores.text += (i + 1) + ".    " + HighScore.names[i] + "   " + HighScore.scores[i] + "\n";
            }
            }
        HighScore.save();

        HighScore.sceneName = "Level2";
        HighScore.load();
        for (int i = 0; i < HighScore.names.Length; i++)
        {
            if (i == 0)
            {
                level2scores.text = (i + 1) + ".    " + HighScore.names[i] + "   " + HighScore.scores[i] + "\n";
            }
            else
            {
                level2scores.text += (i + 1) + ".    " + HighScore.names[i] + "   " + HighScore.scores[i] + "\n";
            }
        }
        HighScore.save();
    }

    public void addName(Text name)
    {
        HighScore.playerName = name.text.ToString();
        if(HighScore.playerName == "")
        {
            HighScore.playerName = "Player";
        }
        HighScore.save();
    }

    public void loadScores()
    {
        HighScore.befSort();
    }
    public void ResetScores()
    {
        HighScore.clear();
    }

    public void Awake()
    {
        display();
    }
}
