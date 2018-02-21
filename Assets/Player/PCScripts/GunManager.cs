using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunItem[] bullet;

    public GameObject[] bulletPool;

    public int weaponIndex = 0;//keeps track of selected weapon via index.

    // Use this for initialization
    void Start()
    {
        //array wide bullet settings here

        //set default bullet settings here
        foreach (GameObject bul in bulletPool)
        {
            
            bul.gameObject.AddComponent<MeshRenderer>();
            bul.GetComponent<MeshRenderer>().material = bullet[weaponIndex].material;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //weapon select here
        //overwrite bullet current settings here
        //Input controls here
        //Projectile cloning here
    }

    public void SwitchMesh()
    {
        foreach (GameObject bul in bulletPool)
        {
            bul.gameObject.AddComponent<MeshRenderer>();
            bul.GetComponent<MeshRenderer>().material = bullet[weaponIndex].material;
        }
    }
}
