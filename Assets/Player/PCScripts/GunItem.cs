using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunItem
{
    //source bullet settings

    public Material material;

    public string name;

    public float damage;

    public float lifetime;

    public float speed;

    public float fireRate = 0.25f;
    //public bool homing;

    //public float splashRadius;

    //public bool unlocked;//determines if the weapons should be availible to the player to use. Not to be mistaken for if the object is active or not.

}
