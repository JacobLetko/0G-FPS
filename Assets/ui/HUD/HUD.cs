using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public float health;
    public int ammo;
    public PlayerStats player;
    public Slider healthbar;
    public GunManager gun;
    public HighScoreBehavior score;
    public Text ammoAmount;

    public void Start()
    {
        health = player.GetHealth();
    }

    private void Update()
    {
        // gunmanager weponindex
        int x = gun.weaponIndex; 
        ammo = gun.currentAmmo;
        healthbar.value = calchealth();
        score.display();
        if (gun.infiniteAmmo == true)
        {
            ammoAmount.text = "Ammo: INF";
        }
        else
        {
            ammoAmount.text = "Ammo: " + ammo;
        }
    }

    float calchealth()
    {
        return player.GetHealth() / health;

    }
}
