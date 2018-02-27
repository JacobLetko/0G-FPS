using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public float health;
    public PlayerStats player;
    public Slider healthbar;

    public void Start()
    {
        health = player.GetHealth();
    }

    private void Update()
    {
        healthbar.value = calchealth();
    }

    float calchealth()
    {
        return player.GetHealth() / health;

    }
}
