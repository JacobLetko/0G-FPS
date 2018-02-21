using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{

    //Place script on Player
    public GunItem[] bullet;

    public GameObject bulletPool;


    public int weaponIndex = 0;//keeps track of selected weapon via index.

    public bool fire = false;

    

    int selectedWeapon;//detects weapon swap


    float timer;

    // Use this for initialization
    void Start()
    {
        selectedWeapon = weaponIndex;
        timer = bullet[weaponIndex].fireRate;
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
                if (timer >= bullet[weaponIndex].fireRate)
                {
                    GameObject bul = bulletPool.GetComponent<BulletPool>().GetBullet();
                    SwitchBullet(bul);
                    //if (bul.activeSelf == false)
                    //{
                        bul.transform.position = transform.position;
                        bul.transform.rotation = transform.rotation;
                        bul.SetActive(true);
                    //}
                    timer = 0;
                }

            }
        }
    }

    public void SwitchBullet(GameObject bulletObj)
    {

        //if (bulletObj.activeSelf == false)
        //{
            bulletObj.GetComponent<MeshRenderer>().materials[0] = bullet[weaponIndex].material;
            bulletObj.GetComponent<Bullet>().damage = bullet[weaponIndex].damage;
            bulletObj.GetComponent<Bullet>().speed = bullet[weaponIndex].speed;
            bulletObj.GetComponent<Bullet>().lifetime = bullet[weaponIndex].lifetime;
        //}

    }


}
