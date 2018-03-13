using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public GameObject explosionParts;
    public GameObject fighterSkin;
    public GameObject deathParticles;
    public AudioSource audioSource;
    public AudioClip damaged;
    public AudioClip deathSound;
    public float hitPoints = 100;
    bool playedDeath = false;
    //public float shieldPoints = 100;
    float currentHP;


    public void Damage(float amt)
    {
        //if (shieldPoints >= 0)
        //{
        //    shieldPoints -= amt;
        //}
        hitPoints -= amt;
        hitPoints = Mathf.Clamp(hitPoints, 0, 100);
        
        if(hitPoints <= 0 && !playedDeath)
        {
            fighterSkin.SetActive(false);
            PlayerCam rig = GetComponent<PlayerCam>();
            rig.enabled = false;

            GunManager gun = GetComponent<GunManager>();
            gun.enabled = false;

            Collider col = GetComponent<Collider>();
            col.enabled = false;



            audioSource.pitch = 0.2f;
            audioSource.PlayOneShot(deathSound, 1f);
            deathParticles.SetActive(true);
            deathParticles.GetComponent<ParticleSystem>().Play();
            explosionParts.SetActive(true);
            playedDeath = true;
            
        }
    }

    public float GetHealth()
    {
        return hitPoints;
    }


    private void Awake()
    {
        explosionParts.SetActive(false);
    }


    void Start()
    {

        
        

        currentHP = hitPoints;
        AudioSource[] array = GetComponents<AudioSource>();
        if(array.Length > 1)
        {
            audioSource = array[1];
        }
    }


    void Update()
    {
        if (currentHP > hitPoints)
        {
            Debug.Log("i was hit");
            currentHP = hitPoints;
            if(!audioSource.isPlaying)
            {
                audioSource.pitch = 3;
                audioSource.PlayOneShot(damaged,0.5f);

                //audioSource.pitch = 1;
            }
            
        }
        if (currentHP < hitPoints)
        {
            currentHP = hitPoints;
        }



        //if (playedDeath == false)
        //{
           
        //    if (currentHP <= 0)
        //    {
        //        fighterSkin.SetActive(false);
        //        Rigidbody rig = GetComponent<Rigidbody>();
        //        rig.drag = 99999;

        //        Collider col = GetComponent<Collider>();
        //        col.enabled = false;

        //        audioSource.pitch = 0.7f;
        //        audioSource.PlayOneShot(deathSound, 0.5f);
        //        explosionParts.SetActive(true);
        //        playedDeath = true;
        //    }
            
        //}


    }


}
