﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winLoss : MonoBehaviour {

    public GameObject goalObj;
    public PlayerStats player;
    private bool invoking;
    private IDamagable goal;


    private void Start()
    {
        goal = goalObj.GetComponent<IDamagable>();
    }

    // Update is called once per frame
    void Update ()
    {
	    if(goal.GetHealth() <= 0)
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Win");
    }

    public void SwitchScreenLose()
    {
        CancelInvoke();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver");
    }
    
}
