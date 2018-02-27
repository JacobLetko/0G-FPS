using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Image foregroundImage;
    public PlayerStats player;
    float hp;

    private void Start()
    {
        hp = player.GetHealth();
    }

    private void Update()
    {
        foregroundImage.fillAmount -= hp;
    }
}
