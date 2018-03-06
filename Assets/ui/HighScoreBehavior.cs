using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreBehavior : MonoBehaviour {

    public Text scoretext;

    public void display()
    {
        scoretext.text = "Score: " + HighScore.score;
    }

    public void table()
    {
            for (int i = 0; i < HighScore.names.Length; i++)
            {
            if (i == 0)
            {
                scoretext.text = (i + 1) + ".    " + HighScore.names[i] + "   " + HighScore.scores[i] + "\n";
            }
            else
            {
                scoretext.text += (i + 1) + ".    " + HighScore.names[i] + "   " + HighScore.scores[i] + "\n";
            }
            }
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
        HighScore.sort();
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
