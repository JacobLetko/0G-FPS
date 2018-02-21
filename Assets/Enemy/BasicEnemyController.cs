using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour, IDamagable {

    public float maxHealth = 100.0f;
    public float rotationForce = 10.0f;

    public Rigidbody body;
    public Transform player;
    public GameObject bulletPrefab;

    private float health;
    private bool alive = true;



	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(alive)
        {

        }
	}

    void FixedUpdate()
    {
        if(alive)
        {
            RaycastHit hit;
            Vector3 targetOffset = player.position - transform.position;
            float angleToPlayer = Vector3.Angle(targetOffset, transform.forward);
            if (angleToPlayer >= -90 && angleToPlayer <= 90 &&
               Physics.Raycast(transform.position, targetOffset, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    float angleDif = Vector3.Angle(transform.position, player.position);
                    Vector3 cross = Vector3.Cross(transform.forward, targetOffset);
                    float rampedSpeed = rotationForce * (cross.magnitude / angleDif);

                    float appliedSpeed = Mathf.Min(rampedSpeed, rotationForce);
                    Vector3 desiredTorque = cross * (appliedSpeed / cross.magnitude);
                    Debug.DrawLine(transform.position, transform.position + (50 * desiredTorque));

                    body.AddTorque(desiredTorque - body.angularVelocity);
                }
            }
            
            if(Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if(hit.transform.CompareTag("Player"))
                {
                    Shoot();
                }
            }
        }
    }



    public void Damage(float amt)
    {
        if(alive)
        {
            health = Mathf.Clamp(health - amt, 0.0f, maxHealth);
            if (health <= 0)
            {
                Kill();
            }
        }
    }

    private void Kill()
    {
        gameObject.SetActive(false);

        //needs an explosion effect or something to let you know it's dead
        //after that's added, switch from SetActive to alive = false

        //alive = false;
    }

    private void Shoot()
    {
        GameObject b = Instantiate(bulletPrefab);
        b.transform.position = transform.position + transform.forward * 2f;
        b.transform.rotation = transform.rotation;
    }

}
