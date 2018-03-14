using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleFade : MonoBehaviour {

    public CanvasRenderer[] renderers;
    public float fadeSpeed = 1.0f;
    public float startWait = 2.0f;

    private float timer = 0;



    private void Update()
    {
        if(timer < startWait)
        {
            timer += Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < renderers.Length; ++i)
            {
                renderers[i].SetAlpha(renderers[i].GetAlpha() - fadeSpeed * Time.deltaTime);
            }
        }
    }


}
