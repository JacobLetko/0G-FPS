using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreBehavior : MonoBehaviour {

    public Text scoretext;

    Scene activescene;
    string sceneName;

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
        Debug.Log("OOOOIIIIIII");
        HighScore.playerName = name.text.ToString();
        HighScore.save();
    }

    public void loadScores()
    {
        HighScore.sort();
    }

    public void Update()
    {
        activescene = SceneManager.GetActiveScene();
        sceneName = activescene.name;

        if(sceneName == "Win")
        {
            display();
        }
    }
}
