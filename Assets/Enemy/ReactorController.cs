using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReactorController : MonoBehaviour, IDamagable
{

    public float maxHealth = 1000.0f;
    public int killScore = 1000;
    
    public AudioClip explodeSound;
    
    public AudioSource audioSource;
    public ParticleSystem explodeEffect;
    public GameObject explodeParts;

    private float health;
    private bool alive = true;



    void Start()
    {
        health = maxHealth;
    }



    public void Damage(float amt)
    {
        if (alive)
        {
            health = Mathf.Clamp(health - amt, 0.0f, maxHealth);
            if (health <= 0)
            {
                Kill();
            }
        }
    }

    public float GetHealth()
    {
        return health;
    }

    private void Kill()
    {
        explodeEffect.Play();
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(explodeSound);
        alive = false;

        HighScore.addPoints(killScore);

        explodeParts.SetActive(true);

        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
    }

}
