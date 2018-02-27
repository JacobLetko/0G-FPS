using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float healAmt = 25.0f;
    public int scoreOnPickup = 10;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if (player)
        {
            player.Damage(-healAmt);
            HighScore.addPoints(scoreOnPickup);
            gameObject.SetActive(false);
        }
    }

}
