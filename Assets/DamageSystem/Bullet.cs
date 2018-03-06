using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage = 10f;
    public float speed = 10f;
    public float lifetime = 2f;
    public float AOE = 0;
    public bool hasTrail = false;
    public AudioSource sfxSource;
    public Renderer renderer;
    public Rigidbody body;
    public Transform sourceObj;
    public Collider collider;
    [HideInInspector]
    public string effectName = "Splode";

    private TrailRenderer trailRenderer;
    private ParticleSystem contactEffect;

    private bool alive = false;



    private void OnEnable()
    {
        alive = true;
        renderer.enabled = true;
        collider.enabled = true;
        contactEffect = null;
        Transform effect = transform.Find(effectName + "(Clone)");
        if (effect != null)
        {
            contactEffect = effect.GetComponent<ParticleSystem>();
            contactEffect.Stop();
            contactEffect.gameObject.SetActive(false);
        }
        

        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
            sfxSource.volume = 0f;
        }

        if (trailRenderer == null)
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }

        trailRenderer.enabled = false;
        Invoke("Kill", lifetime);
        if (hasTrail)
        {
            Invoke("TrailOn", trailRenderer.time + 0.01f);
        }

        body.velocity = transform.forward * speed;
    }

    void TrailOn()
    {       
        trailRenderer.enabled = true;
    }

    public bool IsAlive()
    {
        return alive;
    }

    private void FixedUpdate () {
        if(!alive)
        {
            body.velocity = Vector3.zero;
            if (contactEffect == null)
            {
                gameObject.SetActive(false);
            }
            else if(!contactEffect.IsAlive())
            {
                contactEffect.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(alive && other.transform != sourceObj)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                if (AOE != 0)
                {
                    AreaOfEffect();
                }
                else
                {
                    damagable.Damage(damage);
                }
            }
            else
            {
                if (AOE != 0)
                {
                    AreaOfEffect();
                }
            }
            CancelInvoke();
            Kill();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
    //    if (damagable != null)
    //    {
    //        if (AOE != 0)
    //        {
    //            AreaOfEffect();
    //        }
    //        else
    //        {
    //            damagable.Damage(damage);
    //        }
    //    }
    //    else
    //    {
    //        if (AOE != 0)
    //        {
    //            AreaOfEffect();
    //        }
    //    }
    //    CancelInvoke();
    //    gameObject.SetActive(false);
    //}

    //private void DeactivateInvoke()
    //{
    //    //Kill();
    //    //alive = false;
    //    //if(contactEffect != null)
    //    //{
    //    //    contactEffect.gameObject.SetActive(true);
    //    //    contactEffect.Play();
    //    //}
    //    //sfxSource.volume = 1f;
    //    //sfxSource.mute = false;
    //    //sfxSource.PlayOneShot(sfxSource.clip, 1f);
    //    //gameObject.SetActive(false);
    //}

    private void Kill()
    {
        body.velocity = Vector3.zero;
        alive = false;
        if (contactEffect != null)
        {
            contactEffect.gameObject.SetActive(true);
            contactEffect.Play();
        }
        renderer.enabled = false;
        collider.enabled = false;
        sfxSource.volume = 1f;
        sfxSource.mute = false;
        sfxSource.PlayOneShot(sfxSource.clip, 1f);
    }

    private void AreaOfEffect()
    {

        //Debug.Log("Booom!");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AOE);//Physics.OverlapSphere(Explosion Source,Explosion radius)
        foreach (Collider other in hitColliders)
        {
            if (other != null)
            {
                if (other.GetComponent<Rigidbody>() != null)
                {
                    if (other.tag != "Player")
                    {
                        float dist = (other.transform.position - transform.position).magnitude;

                        IDamagable damagable = other.GetComponent<IDamagable>();
                        if (damagable != null)
                        {
                            damagable.Damage(damage * (1.0f - (dist / AOE)));
                        }

                        other.GetComponent<Rigidbody>().AddExplosionForce(damage * 2, transform.position, AOE);
                    }
                }
            }
        }
    }


}
