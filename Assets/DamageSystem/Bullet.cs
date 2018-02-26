using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage = 10f;
    public float speed = 10f;
    public float lifetime = 2f;



    private void OnEnable()
    {
        Invoke("DeactivateInvoke", lifetime);
    }

    private void FixedUpdate () {
        transform.position += transform.forward * speed * Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if(damagable != null)
        {
            damagable.Damage(damage);
        }
        CancelInvoke();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.Damage(damage);
        }
        CancelInvoke();
        gameObject.SetActive(false);
    }

    private void DeactivateInvoke()
    {
        gameObject.SetActive(false);
    }

}
