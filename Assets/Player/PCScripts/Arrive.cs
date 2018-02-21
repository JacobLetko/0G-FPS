using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour {

   // public float BurstSpeed;
    public float speed;
    public Transform target;
    Rigidbody targetRB;
   // public float projectedDist;
   // Vector3 projectedPos;

    float dist;
    //Vector3 desiredVel;
    Rigidbody myRig;
    float currentSpeed;
    //public float acceptableDist;




    // Use this for initialization
    void Start()
    {

        myRig = GetComponent<Rigidbody>();
        targetRB = target.GetComponent<Rigidbody>();
        currentSpeed = speed;
        StartCoroutine(rotTowards());
    }

    // Update is called once per frame
    //void Update()
    //{

    //    Vector3 targetOffset = target.position - transform.position;



    //    //float dist = Vector3.Distance(transform.position, target.position);
    //    float Angle = Mathf.Atan2(targetOffset.y, targetOffset.x);

    //    float angleDif = Vector3.Angle( transform.forward, targetOffset);

    //    float rampedSpeed = speed * (targetOffset.magnitude / angleDif);
    //    Debug.Log(rampedSpeed);
    //    float clippedSpeed = Mathf.Min(rampedSpeed, speed);

    //    Vector3 crossAngle = Vector3.Cross(transform.forward, targetOffset);

    //    Vector3 desiredVelocity = ((clippedSpeed / targetOffset.magnitude)) * crossAngle;
    //    Debug.DrawLine(transform.position, transform.position + (desiredVelocity * 50));
    //    // myRig.velocity = desiredVelocity;
    //    myRig.AddTorque(desiredVelocity);

    //}

    IEnumerator rotTowards()
    {


        Vector3 targetOffset = target.position - transform.position;
        float angleStart = Vector3.Angle(transform.forward, targetOffset);
        float angleDif = Vector3.Angle(transform.forward, targetOffset);
        while (angleDif != 0)
        {
            angleDif = Vector3.Angle(transform.forward, targetOffset);

            float rampedSpeed = speed * ( angleDif / angleStart);
            Debug.Log(rampedSpeed);
            float clippedSpeed = Mathf.Min(rampedSpeed, speed);

            Vector3 crossAngle = Vector3.Cross(transform.forward, targetOffset);

            Vector3 desiredVelocity = ((clippedSpeed / targetOffset.magnitude)) * crossAngle;
            Debug.DrawLine(transform.position, transform.position + (desiredVelocity * 50));
            // myRig.velocity = desiredVelocity;
            myRig.AddTorque(desiredVelocity);
            yield return null;
        }
        //float dist = Vector3.Distance(transform.position, target.position);
        

       

        yield return null;
    }



        //myRig.AddForce((desiredVel - myRig.velocity) * Time.deltaTime);
    }
