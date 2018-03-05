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
    [HideInInspector]
    public ParticleSystem contactEffect;
    TrailRenderer render;

    private void OnEnable()
    {

        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
            sfxSource.volume = 0f;
        }

        if (render == null)
        {
            render = GetComponent<TrailRenderer>();
        }

        render.enabled = false;
        Invoke("DeactivateInvoke", lifetime);
        if (hasTrail)
        {
            Invoke("TrailOn", render.time + 0.01f);
        }
    }

    void TrailOn()
    {       
        render.enabled = true;
    }

    private void FixedUpdate () {
        transform.position += transform.forward * speed * Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
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
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
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
        gameObject.SetActive(false);
    }

    private void DeactivateInvoke()
    {
        contactEffect.Play();
        sfxSource.volume = 1f;
        sfxSource.mute = false;
        sfxSource.PlayOneShot(sfxSource.clip, 1f);
        gameObject.SetActive(false);
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
