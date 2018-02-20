using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour, IDamagable {

    public float maxHealth = 100.0f;
    public float rotationForce = 10.0f;

    public Rigidbody body;
    public Transform player;

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
            //TAKEN FROM http://wiki.unity3d.com/index.php?title=TorqueLookRotation
            Vector3 targetDelta = player.position - transform.position;

            //get the angle between transform.forward and target delta
            float angleDiff = Vector3.Angle(transform.forward, targetDelta);

            // get its cross product, which is the axis of rotation to
            // get from one vector to the other
            Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

            // apply torque along that axis according to the magnitude of the angle.
            body.AddTorque(cross * angleDiff * rotationForce);
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



}
