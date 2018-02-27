using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable
{

    public float hitPoints = 100;
    //public float shieldPoints = 100;



    public void Damage(float amt)
    {
        //if (shieldPoints >= 0)
        //{
        //    shieldPoints -= amt;
        //}
        hitPoints -= amt;
        Mathf.Clamp(hitPoints, 0, 100);

    }

    public float GetHealth()
    {
        return hitPoints;
    }



}
