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

    public Image laser;
    public Image missle;

    public void Start()
    {
        health = player.GetHealth();
    }

    private void Update()
    {
        ammo = gun.currentAmmo;
        healthbar.value = calchealth();
        score.display();
        if (gun.infiniteAmmo == true)
        {
            ammoAmount.text = "Ammo: INF";
            laser.enabled = true;
            missle.enabled = false;
        }
        else
        {
            ammoAmount.text = "Ammo: " + ammo;
            laser.enabled = false;
            missle.enabled = true;
        }
    }

    float calchealth()
    {
        return player.GetHealth() / health;

    }
}
