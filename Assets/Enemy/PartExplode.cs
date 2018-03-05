using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartExplode : MonoBehaviour {

    public float partScale = 1.0f;


	// Use this for initialization
	void Start () {
        Transform[] parts = GetComponentsInChildren<Transform>(true);
        float minScaleX = transform.parent.localScale.x / parts.Length;
        float minScaleY = transform.parent.localScale.y / parts.Length;
        float minScaleZ = transform.parent.localScale.z / parts.Length;
        float maxScaleX = minScaleX * (parts.Length / 2);
        float maxScaleY = minScaleY * (parts.Length / 2);
        float maxScaleZ = minScaleZ * (parts.Length / 2);
        
        foreach (Transform p in parts)
        {
            p.gameObject.SetActive(true);
            p.parent = null;
            p.localScale  = new Vector3(Random.Range(minScaleX, maxScaleX),
                                        Random.Range(minScaleY, maxScaleY),
                                        Random.Range(minScaleZ, maxScaleZ));
            p.localScale *= partScale;
            p.eulerAngles = new Vector3(Random.Range(0, 360),
                                        Random.Range(0, 360),
                                        Random.Range(0, 360));
        }
	}

}
