using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winLoss : MonoBehaviour {

    public IDamagable reactor;
    public IDamagable player;
    private bool win;
    
    // Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(reactor.GetHealth() <= 0)
        {
            SceneManager.LoadScene("Win");
        }
        else if(player.GetHealth() <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
	}
}
