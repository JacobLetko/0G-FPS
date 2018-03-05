using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //This is the first person cam controls

    [Header("Player Controls")]
    public float acceleration;

    public float HardSpeedLimmit = 30;//in meters per second

    [Range(0, 100)]
    public float mouseSensX = 3.5f;//Sensativity x
    [Range(0, 100)]
    public float mouseSensY = 3.5f;//sensativity Y
    [Range(0, 100)]
    public float tiltSensitivity = 60;
    [Range(0, 1)]
    public float throttle;//adjustable speed limmit. values ranges from 0 to 1.

    [Header("Monitered Variables")]
    public float metersPerSec;//measuring stick
    public float vert;// = Input.GetAxis("Vertical");
    public float horz;// = Input.GetAxis("Horizontal");
    public float depth;
    public float posTilt;
    public float negTilt;
    public bool breaking;
    public float breakDrag = 7;
    public float angularBreakDrag = 7;
    float normalDrag;
    float normalAngularDrag;

    Transform camT;
    Rigidbody myRig;


    // Use this for initialization
    void Start()
    {
        camT = Camera.main.transform;
        camT.transform.position = transform.position + new Vector3(0,0.1f,0);
        camT.rotation = transform.rotation;
        myRig = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        normalDrag = myRig.drag;
        normalAngularDrag = myRig.angularDrag;

    }



    // Update is called once per frame
    void Update()
    {




        vert = 0;
        horz = 0;
        depth = 0;
        posTilt = 0;
        negTilt = 0;

        vert = Input.GetAxis("Vertical");
        horz = Input.GetAxis("Horizontal");
        depth = Input.GetAxis("Jump");
        posTilt = Input.GetAxis("PosZAxisRotate");
        negTilt = -Input.GetAxis("NegZAxisRotate");

        if (posTilt >= 0.3 && negTilt <= -0.3)
        {
            breaking = true;

        }
        else
        {

            breaking = false;
        } 

        

        metersPerSec = myRig.velocity.magnitude;


        myRig.AddRelativeTorque(Vector3.up * Input.GetAxis("Mouse X") * mouseSensX);//do not multiply by time.deltatime as mouse input is frame independant

        myRig.AddRelativeTorque(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensY);


        if (Input.GetKey("x"))
        {
            ThirdPerson();
        }
        else
        {
            camT.transform.position = transform.position + new Vector3(0, 0.1f, 0);
            camT.rotation = transform.rotation;
        }






    }


    void FixedUpdate()
    {     
        
        //breaking
        if (breaking)
        {
            myRig.drag = breakDrag;
            myRig.angularDrag = angularBreakDrag;
            
        }
        else
        {
            myRig.drag = normalDrag;
            myRig.angularDrag = normalAngularDrag;
        }
        //Movement controls
        if (vert >= 0)//(Input.GetKey(KeyCode.W))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(0, 0, acceleration * throttle));// * Time.deltaTime);
            }
        }
        if (vert <= 0)//(Input.GetKey(KeyCode.S))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(0, 0, -acceleration * throttle));// * Time.deltaTime);
            }
        }
        if (horz >= 0)//(Input.GetKey(KeyCode.D))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(acceleration * throttle, 0, 0));// * Time.deltaTime);
            }
        }
        if (horz <= 0)//(Input.GetKey(KeyCode.A))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(-acceleration * throttle, 0, 0));// * Time.deltaTime);
            }
        }
        if (depth >= 0)//(Input.GetKey(KeyCode.Space))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(0, acceleration * throttle, 0));// * Time.deltaTime);
            }
        }
        if (depth <= 0)//(Input.GetKey(KeyCode.C))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(0, -acceleration * throttle, 0));// * Time.deltaTime);
            }
        }








        //cam tilt controls
        //if (tilt >= 0)//(Input.GetKey(KeyCode.E))
        //{
            myRig.AddRelativeTorque(Vector3.forward * Input.GetAxis("PosZAxisRotate") * -tiltSensitivity);//(Vector3.forward * ((-acceleration / 3) * Time.deltaTime));
            myRig.AddRelativeTorque(Vector3.forward * -Input.GetAxis("NegZAxisRotate") * -tiltSensitivity);//(Vector3.forward * ((-acceleration / 3) * Time.deltaTime));
        //}








    }


    private void ThirdPerson()
    {

        RaycastHit hitCollider;    
        bool hit = Physics.Raycast(transform.position, transform.up, out hitCollider, 1f);

        RaycastHit hitCollider2;
        bool hit2 = Physics.Raycast(transform.position, -transform.forward, out hitCollider2, 1f);

        camT.transform.localPosition = new Vector3(0, 1, -1);

        if (hit)
        {
            Debug.Log("hit");
            float distance = Vector3.Distance(transform.position, hitCollider.point);

            camT.transform.localPosition -= new Vector3(0, (camT.transform.localPosition.y - distance), 0);

            //if (distance >= 0.35)
            //{
            //    camT.transform.localPosition -= new Vector3(0, (camT.transform.localPosition.y - distance) + (0.1f  * distance - camT.transform.localPosition.y) , 0);
            //}
            //else
            //{
            //    camT.transform.localPosition -= new Vector3(0, camT.transform.localPosition.y - (distance - 0.3f), 0);
            //}

        }

        if (hit2)
        {
            float distance = Vector3.Distance(transform.position, hitCollider2.point) + 0.3f;
            Debug.Log("hit2");

            camT.transform.localPosition -= new Vector3(0, 0, camT.transform.localPosition.z - distance);


            //if (distance >= 0.35)
            //{
            //    camT.transform.localPosition -= new Vector3(0, 0, camT.transform.localPosition.z - distance);
            //}
            //else
            //{
            //    camT.transform.localPosition -= new Vector3(0, 0, camT.transform.localPosition.z - (distance - 0.3f));
            //}
        }




    }
}
