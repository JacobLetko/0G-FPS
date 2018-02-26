using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class GunManager : MonoBehaviour
{

    //Place script on Player
    public GunItem[] bullet;

    public GameObject bulletPool;
    
    public AudioSource playerSourceAudio;



    //[HideInInspector]
    //[Range(0, 1)]
    //public float myVolume = 1;
    //[Range(0, 3)]
    //public float myPitch = 1;

    //public bool isAudioloop;


    public int weaponIndex = 0;//keeps track of selected weapon via index.

    public bool fire = false;

    

    int selectedWeapon;//detects weapon swap


    float timer;
    //[HideInInspector]
    public int currentAmmo;
    [HideInInspector]
    public bool infiniteAmmo = false;


    //void Awake()
    //{
    //    playerSourceAudio = gameObject.AddComponent<AudioSource>();
    //}

    // Use this for initialization
    void Start()
    {
        playerSourceAudio = GetComponent<AudioSource>();

        if (bulletPool == null)
        {
            if (GameObject.Find("BulletPoolObj") != null)
            {
                bulletPool = GameObject.Find("BulletPoolObj");
            }
            else
            {
                Debug.LogError("Error: Cannot Find Bullet Pool Object");
            }
        }

        selectedWeapon = weaponIndex;
        timer = bullet[weaponIndex].fireRate;
        currentAmmo = bullet[weaponIndex].ammo;
        //set default bullet settings here


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Input controls here
        weaponIndex = (int)Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetAxis("Fire1") > 0)
        {
            fire = true;
        }
        else
        {
            fire = false;
        }

        //weapon select here
        //overwrite bullet current settings here
        if (selectedWeapon != weaponIndex)
        {
            selectedWeapon = weaponIndex;
        }


        //Projectile cloning here

        if (fire)
        {

            if (bulletPool != null)
            {
                if (currentAmmo > 0)
                {
                    if (timer >= bullet[weaponIndex].fireRate)
                    {
                        GameObject bul = bulletPool.GetComponent<BulletPool>().GetBullet();
                        SwitchBullet(bul);
                        SwitchSound();

                        bul.transform.position = transform.position;
                        bul.transform.rotation = transform.rotation;
                        bul.SetActive(true);

                        playerSourceAudio.Play();

                        if (!infiniteAmmo)
                        {
                            currentAmmo--;
                        }

                        timer = 0;
                    }
                }
            }
        }
    }

    void SwitchBullet(GameObject bulletObj)
    {


        bulletObj.GetComponent<MeshRenderer>().material = bullet[weaponIndex].material;
        bulletObj.GetComponent<Bullet>().damage = bullet[weaponIndex].damage;
        bulletObj.GetComponent<Bullet>().speed = bullet[weaponIndex].speed;
        bulletObj.GetComponent<Bullet>().lifetime = bullet[weaponIndex].lifetime;
        currentAmmo = bullet[weaponIndex].ammo;
        infiniteAmmo = bullet[weaponIndex].infinite;

    }

    void SwitchSound()
    {

        playerSourceAudio.clip = bullet[weaponIndex].clip;
        playerSourceAudio.volume = bullet[weaponIndex].volume;
        playerSourceAudio.pitch = bullet[weaponIndex].pitch;
        playerSourceAudio.loop = bullet[weaponIndex].loop;

    }




}
