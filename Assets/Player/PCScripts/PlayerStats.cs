using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public AudioSource audioSource;
    public AudioClip damaged;
    public float hitPoints = 100;
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
        

    }

    public float GetHealth()
    {
        return hitPoints;
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
    }


}
