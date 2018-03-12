using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class GunItem
{
    //source bullet settings


    

    [Header("Core Mechanics")]
    [Tooltip("Enabling isBeam negates the Speed, lifetime, accuracyModifier, and HasTrail variables." +
        "\n0 = 0ff," +
        "\n1 = pulse(Obeys fireRate)," +
        "\n2 = on(ignores fireRate in favor of damage/time)")]
    [Range(0,2)]
    public int beamType;
    public string name;


    //if Isbeam is false these are active--------------------------
    [Header("Projectile only")]
    public float speed;  
    public float lifetime;
    public bool hasTrail = false;
    [Range(0, 100)]
    public float accuracyModifier = 1;
    //-------------------------------------------------------------
    [Space]
    [Header("Universal Gun Variables")]
    public AudioClip contactSound;

    public string effectName;

    public Material material;

    public float damage;

    public float fireRate = 0.25f;

    [Range(1, 100000)]
    public int ammo = 100;

    public bool infinite = false;

    public float splashRadius = 0;



    //public bool homing;



    //public bool unlocked;//determines if the weapons should be availible to the player to use. Not to be mistaken for if the object is active or not.

    //Audio------------------------------------------

    [Header("Audio")]

    public AudioClip clip;

    //public string AudioName;

    [Range(0, 1)]
    public float volume = 1;
    [Range(0, 3)]
    public float pitch = 1;

    [Tooltip("Loop is recomended for continuouse laser beams or high rate of fire weapons")]
    public bool loop = false;




    //[HideInInspector]
    //public AudioSource source;



    //-----------------------------------------------

}
