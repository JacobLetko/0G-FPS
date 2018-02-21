﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunItem
{
    //source bullet settings
   
    public string name;

    public float damage;

    public float speed;

    //public bool homing;

    public float splashRadius;

    public bool unlocked;//determines if the weapons should be availible to the player to use.

}
