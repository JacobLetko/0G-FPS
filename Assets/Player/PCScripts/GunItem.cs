using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class GunItem
{
    //source bullet settings
    [Header("Core Mechanics")]

    public Material material;

    public string name;

    public float damage;

    public float lifetime;

    public float speed;

    public float fireRate = 0.25f;

    [Range(1,100000)]
    public int ammo = 100;

    public bool infinite = false;



    //public bool homing;

    //public float splashRadius;

    //public bool unlocked;//determines if the weapons should be availible to the player to use. Not to be mistaken for if the object is active or not.

    //Audio------------------------------------------

    [Header("Audio")]

    public AudioClip clip;

    //public string AudioName;

    [Range(0, 1)]
    public float volume = 1;
    [Range(0, 3)]
    public float pitch = 1;

    public bool loop = false;


    //[HideInInspector]
    //public AudioSource source;



    //-----------------------------------------------

}
