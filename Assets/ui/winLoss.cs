﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winLoss : MonoBehaviour {

    public ReactorController reactor;
    public PlayerStats player;
    private bool invoking;
    
    // Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(reactor.GetHealth() <= 0)
        {
            if (invoking == false)
            {
                Invoke("SwitchScreenWin", 3.0f);
                invoking = true;
            }
        }
        else if(player.GetHealth() <= 0)
        {
            if (invoking == false)
            {
                Invoke("SwitchScreenLose", 3.0f);
                invoking = true;
            }
        }
	}

    public void SwitchScreenWin()
    {
        CancelInvoke();
        SceneManager.LoadScene("Win");
    }

    public void SwitchScreenLose()
    {
        CancelInvoke();
        SceneManager.LoadScene("GameOver");
    }
    
}
