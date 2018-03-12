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

    public GameObject laser;
    public GameObject missle;

    private bool hit;
    public GameObject bars;
    private float prevHealth;

    public void Start()
    {
        health = player.GetHealth();
        prevHealth = health;
        hit = false;
    }

    private void Update()
    {
        ammo = gun.currentAmmo;
        healthbar.value = calchealth();
        score.display();
        if (gun.infiniteAmmo == true)
        {
            ammoAmount.text = "Ammo: INF";
            laser.SetActive(true);
            missle.SetActive(false);
        }
        else
        {
            ammoAmount.text = "Ammo: " + ammo;
            laser.SetActive(false);
            missle.SetActive(true);
        }

        if(hit == true)
        {
            //opacity = .5
            Image[] images = bars.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                //img.material.color.a -= .05f;
                var imgChange = img.color;
                imgChange.a = .5f;
                img.color = imgChange;
            }
            hit = false;
        }
        else
        {
            Image[] images = bars.GetComponentsInChildren<Image>();
            foreach(Image img in images)
            {
                //img.material.color.a -= .05f;
                var imgChange = img.color;
                imgChange.a -= .05f;
                img.color = imgChange;
            }
            //Debug.Log(bars.GetComponent<Renderer>().material.color);
            //opacity -= .05
        }
    }

    float calchealth()
    {
        if (player.GetHealth() < prevHealth)
        {
            hit = true;
        }
        prevHealth = player.GetHealth();
        return player.GetHealth() / health;
    }
}
