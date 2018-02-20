using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //This is the first person cam controls

    [Header("Player Controls")]
    public float acceleration;
    [Range(0,1)]
    public float throttle;//adjustable speed limmit. values ranges from 0 to 1.
    public float HardSpeedLimmit = 30;//in meters per second
    public float metersPerSec;//measuring stick

    float vert;// = Input.GetAxis("Vertical");
    float horz;// = Input.GetAxis("Horizontal");
    float depth;

    public float mouseSensX = 3.5f;//Sensativity x
    public float mouseSensY = 3.5f;//sensativity Y

    Transform camT;
    Rigidbody myRig;
    float vertLookRotation;

    // Use this for initialization
    void Start()
    {
        camT = Camera.main.transform;
        myRig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vert = 0;
        horz = 0;
        depth = 0;


        metersPerSec = myRig.velocity.magnitude;


        myRig.AddRelativeTorque(Vector3.up * Input.GetAxis("Mouse X") * mouseSensX);//do not multiply by time.deltatime as mouse input is frame independant

        myRig.AddRelativeTorque(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensY);


        vert = Input.GetAxis("Vertical");
        horz = Input.GetAxis("Vertical");
        depth = Input.GetAxis("Jump");





        //Movement controls
        if (vert >= 0.1f)//(Input.GetKey(KeyCode.W))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(0, 0, acceleration * throttle) * Time.deltaTime);
            }
        }
        if (vert <= -0.1f)//(Input.GetKey(KeyCode.S))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(0, 0, -acceleration * throttle) * Time.deltaTime);
            }
        }
        if (horz >= 0.1f)//(Input.GetKey(KeyCode.D))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(acceleration * throttle, 0, 0) * Time.deltaTime);
            }
        }
        if (horz <= -0.1f)//(Input.GetKey(KeyCode.A))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(-acceleration * throttle, 0, 0) * Time.deltaTime);
            }
        }
        if (depth >= 0.1f)//(Input.GetKey(KeyCode.Space))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(0, acceleration * throttle, 0) * Time.deltaTime);
            }
        }
        if (depth <= 0.1f)//(Input.GetKey(KeyCode.C))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(0, -acceleration * throttle, 0) * Time.deltaTime);
            }
        }








        //cam tilt controls
        if (Input.GetKey(KeyCode.E))
        {
            myRig.AddRelativeTorque(Vector3.forward * ((-acceleration / 3) * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            myRig.AddRelativeTorque(Vector3.forward * ((acceleration / 3) * Time.deltaTime));
        }
    }
}
