using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    public GameObject pause;
    public bool ispause;

    // Use this for initialization
    void Start()
    {
        ispause = false;
        pause.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && ispause == false)
        {
            Time.timeScale = 0.0f;
            pause.gameObject.SetActive(true);
            ispause = true;
        }
    }

    public void isPause(bool yea)
    {
        ispause = yea;
        Time.timeScale = 1f;
    }
}
