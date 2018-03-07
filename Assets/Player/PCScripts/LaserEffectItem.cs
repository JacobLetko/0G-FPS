using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffectItem : MonoBehaviour
{
    private ParticleSystem contactEffect;
    public string effectName;
    public float lifetime;
    public bool alive = false;

    private void OnEnable()
    {
        Transform effect = transform.Find(effectName + "(Clone)");
        if (effect != null)
        {
            contactEffect = effect.GetComponent<ParticleSystem>();
            contactEffect.gameObject.SetActive(true);
            contactEffect.Play();
        }
        Invoke("Kill", lifetime);
    }


    private void FixedUpdate()
    {
        if (!alive)
        {
            if (contactEffect == null)
            {
                gameObject.SetActive(false);
            }
            else if (!contactEffect.IsAlive())
            {
                gameObject.SetActive(false);
            }
        }
    }



    private void Kill()
    {
        alive = false;
        if (contactEffect != null)
        {
            contactEffect.gameObject.SetActive(false);
            contactEffect.Stop();
        }
    }
}
