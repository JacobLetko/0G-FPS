using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {

    [Header("Score")]
    public int score;
    public List<int> scores;
    public List<string> names;

    [Header("points")]
    public int Epoints;
    public bool levelComplete;
    public int health;
    public int shileds;
    public int ammo;
    
    // Use this for initialization
	void Start ()
    {
        resetScore();
        getList();
	}

    void Update()
    {
        
    }

    public void resetScore()
    {
        score = 0;
    }

    void getList()
    {
        for (int i = 0; i < 10; i++)
        {
            scores.Add(PlayerPrefs.GetInt("scores" + i));
            names.Add(PlayerPrefs.GetString("names" + i));
        }
    }
}
