using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

    public int ammoAmt = 1;
    public int ammoType = 1;
    public int scoreOnPickup = 10;

    public GameObject audioPlayer;


    private void OnCollisionEnter(Collision collision)
    {
        GunManager player = collision.gameObject.GetComponent<GunManager>();
        if (player)
        {
            player.bullet[ammoType].ammo += ammoAmt;
            HighScore.addPoints(scoreOnPickup);
            audioPlayer.SetActive(true);
            audioPlayer.transform.parent = null;
            gameObject.SetActive(false);
        }
    }

}
