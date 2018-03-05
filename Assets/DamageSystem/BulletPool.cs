using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BulletPool : MonoBehaviour {

    public int initialSize = 200;
    public GameObject bulletPrefab;
    public GameObject[] particleEffects;
    
    private GameObject[] pool;



    private void Start()
    {
        pool = new GameObject[initialSize];
        for(int i = 0; i < initialSize; ++i)
        {
            pool[i] = Instantiate(bulletPrefab);
            foreach(GameObject o in particleEffects)
            {
                GameObject g = Instantiate(o);
                g.transform.parent = pool[i].transform;
                g.transform.position = pool[i].transform.position;
                g.transform.rotation = pool[i].transform.rotation;
                g.SetActive(false);
            }
            pool[i].SetActive(false);
        }
    }

    public GameObject GetBullet()
    {
        GameObject ret = null;

        foreach(GameObject b in pool)
        {
            if(!b.activeInHierarchy)
            {
                ret = b;
                break;
            }
        }

        if(ret == null)
        {
            int oldsize = pool.Length;
            Array.Resize(ref pool, oldsize * 2);
            for (int i = oldsize; i < pool.Length; ++i)
            {
                pool[i] = Instantiate(bulletPrefab);
                pool[i].SetActive(false);
            }
            ret = pool[oldsize];
        }

        return ret;
    }

}
