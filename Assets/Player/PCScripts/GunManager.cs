using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class GunManager : MonoBehaviour
{

    //Place script on Player

    
    
    public LineRenderer LineRend;

    public GunItem[] bullet;


    //public Light playerGunLight;



    public GameObject laserEffectPool;
    public GameObject bulletPool;


    public AudioSource playerSourceAudio;

    public float randomPitchRangeModifier = 0.2f;



    public int weaponIndex = 0;//keeps track of selected weapon via index.

    public bool fire = false;



    float timer;
    //[HideInInspector]
    public int currentAmmo;
    [HideInInspector]
    public bool infiniteAmmo = false;




    // Use this for initialization
    void Start()
    {

        LineRend = GetComponent<LineRenderer>();

        LineRend.SetPosition(0, transform.position);




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

        //selectedWeapon = weaponIndex;
        timer = bullet[weaponIndex].fireRate;
        currentAmmo = bullet[weaponIndex].ammo;
        infiniteAmmo = bullet[weaponIndex].infinite;

        //set default bullet settings here


    }

    // Update is called once per frame
    void Update()
    {


        currentAmmo = bullet[weaponIndex].ammo;
        timer += Time.deltaTime;
        //Input controls here

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponIndex < bullet.Length - 1)
            {
                weaponIndex++;
            }
            else
            {
                weaponIndex = 0;
            }
            infiniteAmmo = bullet[weaponIndex].infinite;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponIndex > 0)
            {
                weaponIndex--;
            }
            else
            {
                weaponIndex = bullet.Length - 1;
            }
            infiniteAmmo = bullet[weaponIndex].infinite;
        }






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
        //if (selectedWeapon != weaponIndex)
        //{
        //    selectedWeapon = weaponIndex;

        //}


        //Projectile cloning here

        LineRend.SetPosition(0, transform.position);
        if (LineRend.enabled)
        {
            LineRend.enabled = false;
        }

        if (fire)
        {
            SwitchSound();

            if (bulletPool != null)
            {
                if (bullet[weaponIndex].ammo > 0)
                {
                    if (timer >= bullet[weaponIndex].fireRate && bullet[weaponIndex].beamType == 0)
                    {


                        GameObject bul = bulletPool.GetComponent<BulletPool>().GetBullet();



                        SwitchBullet(bul);

                        bul.transform.position = transform.position + (transform.forward * 1.5f);
                        bul.transform.rotation = transform.rotation;
                        bul.transform.Rotate(new Vector3(Random.Range(bullet[weaponIndex].accuracyModifier, -bullet[weaponIndex].accuracyModifier),
                                                         Random.Range(bullet[weaponIndex].accuracyModifier, -bullet[weaponIndex].accuracyModifier),
                                                         Random.Range(bullet[weaponIndex].accuracyModifier, -bullet[weaponIndex].accuracyModifier)));
                        bul.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

                        bul.SetActive(true);
                        if (bullet[weaponIndex].loop)
                        {
                            if (!playerSourceAudio.isPlaying)
                            {
                                playerSourceAudio.Play();
                            }

                        }
                        else
                        {
                            playerSourceAudio.PlayOneShot(bullet[weaponIndex].clip, playerSourceAudio.volume);
                        }

                        timer = 0;
                        //playerSourceAudio.Play();



                        if (!infiniteAmmo)
                        {
                            bullet[weaponIndex].ammo -= 1;
                        }


                    }
                    else if (bullet[weaponIndex].beamType > 0)
                    {


                        switch (bullet[weaponIndex].beamType)
                        {
                            case 1:
                                PulseLaser();
                                break;
                            case 2:
                                BeamCast();
                                break;


                            default:
                            case 0:
                                Debug.LogError("Beam Type Is 0. Unable to Cast Beam. Check If logic.");
                                break;
                        }




                        //BeamCast();

                        if (bullet[weaponIndex].loop)
                        {
                            if (!playerSourceAudio.isPlaying)
                            {
                                playerSourceAudio.Play();
                            }
                        }



                    }


                }
            }
        }
        else
        {
            playerSourceAudio.Stop();
        }


        currentAmmo = bullet[weaponIndex].ammo;
    }

    void SwitchBullet(GameObject bulletObj)
    {

        //bulletObj.GetComponent<MeshRenderer>().material = bullet[weaponIndex].material;
        bulletObj.GetComponent<Bullet>().damage = bullet[weaponIndex].damage;
        bulletObj.GetComponent<Bullet>().speed = bullet[weaponIndex].speed;
        bulletObj.GetComponent<Bullet>().lifetime = bullet[weaponIndex].lifetime;
        bulletObj.GetComponent<Bullet>().AOE = bullet[weaponIndex].splashRadius;
        bulletObj.GetComponent<Bullet>().hasTrail = bullet[weaponIndex].hasTrail;
        bulletObj.GetComponent<Bullet>().sourceObj = transform;
        bulletObj.GetComponent<Bullet>().effectName = bullet[weaponIndex].effectName;
        Debug.Log(bulletObj.GetComponent<Bullet>().effectName);
        bulletObj.GetComponent<Bullet>().sfxSource.mute = true;
        bulletObj.GetComponent<Bullet>().sfxSource.clip = bullet[weaponIndex].contactSound;
        bulletObj.GetComponent<Bullet>().explosionForce = bullet[weaponIndex].explosionForceModifier;


    }

    void SwitchSound()
    {

        playerSourceAudio.clip = bullet[weaponIndex].clip;
        playerSourceAudio.volume = bullet[weaponIndex].volume;
        playerSourceAudio.pitch = bullet[weaponIndex].pitch + Random.Range(randomPitchRangeModifier, -randomPitchRangeModifier);
        playerSourceAudio.loop = bullet[weaponIndex].loop;

    }




    void PulseLaser()
    {
        LineRend.material = bullet[weaponIndex].material;
        if (timer >= bullet[weaponIndex].fireRate)
        {

            RaycastHit hitCollider;
            bool hit = Physics.Raycast(transform.position, transform.forward, out hitCollider);//Physics.OverlapSphere(Explosion Source,Explosion radius)

            if (hit)
            {
                GameObject las = laserEffectPool.GetComponent<LaserEffectPool>().GetLaserEffectObject();
                //Debug.Log("Las");
                las.GetComponent<LaserEffectItem>().effectName = bullet[weaponIndex].effectName;
                las.transform.position = hitCollider.point;
                las.SetActive(true);

                LineRend.SetPosition(1, hitCollider.point);

                LineRend.enabled = true;

                if (bullet[weaponIndex].splashRadius > 0)
                {
                    AreaOfEffect_Beam(hitCollider.point, bullet[weaponIndex].splashRadius);
                }

                IDamagable damagable = hitCollider.transform.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.Damage(bullet[weaponIndex].damage);
                }
                //Debug.Log("Beam type 1");

            }
            else
            {

                Vector3 endpos = transform.position + transform.forward * 1000.0f;
                LineRend.SetPosition(1, endpos);
                LineRend.enabled = true;
            }

            if (!bullet[weaponIndex].loop)
            {
                playerSourceAudio.PlayOneShot(bullet[weaponIndex].clip, playerSourceAudio.volume);
            }


            if (!infiniteAmmo)
            {
                bullet[weaponIndex].ammo -= 1;
            }
            timer = 0;
        }
    }

    void BeamCast()
    {
        LineRend.material = bullet[weaponIndex].material;

        RaycastHit hitCollider;
        bool hit = Physics.Raycast(transform.position, transform.forward, out hitCollider);//Physics.OverlapSphere(Explosion Source,Explosion radius)
                                                                                           //Debug.Log("type 2");
        if (hit)
        {
            if (hitCollider.transform.tag != "Player")
            {
                LineRend.SetPosition(1, hitCollider.point);

                LineRend.enabled = true;

                if (bullet[weaponIndex].splashRadius > 0)
                {
                    AreaOfEffect_Beam(hitCollider.point, bullet[weaponIndex].splashRadius);
                }

                IDamagable damagable = hitCollider.transform.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.Damage(bullet[weaponIndex].damage * Time.deltaTime);
                }
            }


        }
        else
        {

            Vector3 endpos = transform.position + transform.forward * 1000.0f;
            LineRend.SetPosition(1, endpos);
            LineRend.enabled = true;

        }

        if (timer >= bullet[weaponIndex].fireRate)
        {

            if (hit)
            {
                GameObject las = laserEffectPool.GetComponent<LaserEffectPool>().GetLaserEffectObject();
                las.GetComponent<LaserEffectItem>().effectName = bullet[weaponIndex].effectName;
                las.transform.position = hitCollider.point;
                las.SetActive(true);
            }

            if (!bullet[weaponIndex].loop)
            {
                playerSourceAudio.PlayOneShot(bullet[weaponIndex].clip, playerSourceAudio.volume);
            }

            if (!infiniteAmmo)
            {
                bullet[weaponIndex].ammo -= 1;
            }
            timer = 0;
        }


        //float dist = (hitCollider.transform.position - transform.position).magnitude;
    }

    private void AreaOfEffect_Beam(Vector3 pos1, float AOE)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos1, AOE);//Physics.OverlapSphere(Explosion Source,Explosion radius)
        foreach (Collider other in hitColliders)
        {
            if (other != null)
            {
                if (other.GetComponent<Rigidbody>() != null)
                {
                    if (other.tag != "Player")
                    {
                        float dist = (other.transform.position - pos1).magnitude;

                        IDamagable damagable = other.GetComponent<IDamagable>();
                        if (damagable != null)
                        {
                            damagable.Damage(bullet[weaponIndex].damage * (1.0f - (dist / AOE)));
                        }

                        other.GetComponent<Rigidbody>().AddExplosionForce( (1 + bullet[weaponIndex].damage) * bullet[weaponIndex].explosionForceModifier, pos1, AOE);
                        other.GetComponent<Rigidbody>().AddTorque(pos1 * (1 + bullet[weaponIndex].damage * bullet[weaponIndex].explosionForceModifier), ForceMode.Force);
                    }
                }
            }
        }
    }


}
