using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

    public int ammoAmt = 1;
    public int ammoType = 1;


    private void OnCollisionEnter(Collision collision)
    {
        GunManager player = collision.gameObject.GetComponent<GunManager>();
        if (player)
        {
            player.bullet[ammoType].ammo += ammoAmt;
            gameObject.SetActive(false);
        }
    }

}
