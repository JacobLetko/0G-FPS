using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{

    //Place script on Player
    public GunItem[] bullet;



    public int weaponIndex = 0;//keeps track of selected weapon via index.

    public bool fire = false;

    int selectedWeapon;//detects weapon swap

    // Use this for initialization
    void Start()
    {
        selectedWeapon = weaponIndex;


        //set default bullet settings here
        GameObject baseBullet = Instantiate(new GameObject());
        

    }

    // Update is called once per frame
    void Update()
    {
        
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
            //foreach (GameObject bul in bulletPool)
            //{
            //    if (bul != null)
            //    {
            //        SwitchBullet(bul);
            //        if (bul.activeSelf == false)
            //        {
            //            bul.transform.position = transform.position;
            //            bul.transform.rotation = transform.rotation;
            //            bul.SetActive(true);
            //            return;
            //        }
            //    }

            //}
        }

    }

    public void SwitchBullet(GameObject bulletObj)
    {
        //foreach (GameObject bul in bulletPool)
        //{
        //    if (bul.activeSelf == false)
        //    {
        //        bul.GetComponent<MeshRenderer>().materials[0] = bullet[weaponIndex].material;
        //        bul.GetComponent<Bullet>().damage = bullet[weaponIndex].damage;
        //        bul.GetComponent<Bullet>().speed = bullet[weaponIndex].speed;
        //        bul.GetComponent<Bullet>().lifetime = bullet[weaponIndex].lifetime;
        //    }
        //}
    }

    public void Shoot()
    {

    }

}
