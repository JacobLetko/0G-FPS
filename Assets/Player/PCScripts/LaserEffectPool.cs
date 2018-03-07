using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LaserEffectPool : MonoBehaviour
{
   
    public GameObject[] laserEffects;//stores each object reference to draw effects from
    private GameObject[] pool;//The pool of empty game objects of which to applie the effects too 


    private bool alive = false;
    //private ParticleSystem contactEffect;


    public int initialSize = 200;
    public GameObject laserEffectObj;//stores basic object to apply effect too

    // Use this for initialization
    void Start()
    {
        pool = new GameObject[initialSize];
        for (int i = 0; i < initialSize; ++i)
        {
            pool[i] = Instantiate(laserEffectObj);
            foreach (GameObject o in laserEffects)
            {
                GameObject g = Instantiate(o);
                g.transform.parent = pool[i].transform;
                g.transform.position = pool[i].transform.position;
                g.transform.rotation = pool[i].transform.rotation;
                g.SetActive(false);
                //Debug.Log("Effects:" + i);
            }
            pool[i].SetActive(false);
            //Debug.Log("Objects:" + i);
        }


    }

    // Update is called once per frame


    public bool IsAlive()
    {
        return alive;
    }

    public GameObject GetLaserEffectObject()
    {
        GameObject ret = null;

        foreach (GameObject b in pool)
        {
            if (!b.activeInHierarchy)
            {
                ret = b;
                break;
            }
        }

        if (ret == null)
        {
            int oldsize = pool.Length;
            Array.Resize(ref pool, oldsize * 2);
            for (int i = oldsize; i < pool.Length; ++i)
            {
                pool[i] = Instantiate(laserEffectObj);
                pool[i].SetActive(false);
            }
            ret = pool[oldsize];
        }

        return ret;
    }
}
