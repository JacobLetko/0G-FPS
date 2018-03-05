using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {

    public Vector3 rotSpeed = Vector3.zero;

	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotSpeed * Time.deltaTime);
    }
}
