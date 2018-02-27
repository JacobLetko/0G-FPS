using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreBehavior : MonoBehaviour {

    public Text scoretext;

    public void display()
    {
        scoretext.text = "" + HighScore.score;
    }

    public void table()
    {
        for (int i = 0; i < 10; i++)
        {
            scoretext.text = "1.    " + HighScore.names[i] + "   " + HighScore.scores[i] + "\n";
        }
    }

    public void addName(Text name)
    {
        HighScore.playerName = name.ToString();
    }
}
