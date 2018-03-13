using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float healAmt = 25.0f;
    public int scoreOnPickup = 10;

    public GameObject audioPlayer;


    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
        if (player)
        {
            player.Damage(-healAmt);
            HighScore.addPoints(scoreOnPickup);
            audioPlayer.SetActive(true);
            audioPlayer.transform.parent = null;
            gameObject.SetActive(false);
        }
    }

}
