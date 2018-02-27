using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    public GameObject pause;
    public bool ispause;

    public HighScoreBehavior score;

    // Use this for initialization
    void Start()
    {
        ispause = false;
        pause.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab") && ispause == false)
        {
            ispause = true;
            cursor();
            Time.timeScale = 0.0f;
            pause.gameObject.SetActive(true);
        }

        else if (Input.GetKeyDown("tab") && ispause == true)
        {
            ispause = false;
            cursor();
            Time.timeScale = 1.0f;
            pause.gameObject.SetActive(false);
        }

        score.display();
    }

    public void isPause(bool yea)
    {
        ispause = yea;
        Time.timeScale = 1f;
    }

    public void cursor()
    {
        if(ispause == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (ispause == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
           
        }
    }
}
