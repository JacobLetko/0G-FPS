using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //This is the first person cam controls

    [Header("Assets")]
    public AudioSource audioSource;
    public AudioClip bounce;

    [Header("Player Controls")]
    public float acceleration;

    public float HardSpeedLimmit = 30;//in meters per second

    public float thirdPersonCameraDist = 5.0f;

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


        if (audioSource != null)
        {
            if (audioSource.isPlaying == true)
            {
                audioSource.Stop();
            }
        }

    }



    // Update is called once per frame
    void Update()
    {


        //if (playClip)//((audioStopTimer > 0.1f))
        //{
        //    if (audioSource.isPlaying == false)
        //    {
        //        audioSource.Play();
        //    }


        //}
        //else
        //{
        //    if (audioSource.isPlaying == true)
        //    {
        //        audioSource.Stop();
        //    }

        //}


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

      //  Debug.Log("E input : " + posTilt);
      //  Debug.Log("Q input : " + negTilt);


        if (posTilt >= 0.3 && negTilt <= -0.3 || (Input.GetKey("left shift")))
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
            camT.transform.position = transform.position + transform.up * 0.1f;//new Vector3(0, 0.1f, 0);
            camT.rotation = transform.rotation;
        }

        if ((Input.GetAxis("Vertical") != 0) || (Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Jump") != 0) || (Input.GetAxis("PosZAxisRotate") != 0) || (Input.GetAxis("NegZAxisRotate") != 0))
        {
            if (audioSource.isPlaying == false)
            {

                audioSource.Play();
            }
            audioSource.volume = Mathf.Clamp(0.5f / metersPerSec, 0.5f, 3);
        }
        else
        {
            if (audioSource.isPlaying == true)
            {
                audioSource.Stop();
            }
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
                Debug.Log("CNTRL + E");
            }
        }
        if (depth <= 0)//(Input.GetKey(KeyCode.C))
        {
            if (metersPerSec < HardSpeedLimmit)
            {
                myRig.AddRelativeForce(new Vector3(0, -acceleration * throttle, 0));// * Time.deltaTime);
                Debug.Log("CNTRL + Q");
            }
        }








        //cam tilt controls

        myRig.AddRelativeTorque(Vector3.forward * Input.GetAxis("PosZAxisRotate") * -tiltSensitivity);//(Vector3.forward * ((-acceleration / 3) * Time.deltaTime));
        myRig.AddRelativeTorque(Vector3.forward * -Input.GetAxis("NegZAxisRotate") * -tiltSensitivity);//(Vector3.forward * ((-acceleration / 3) * Time.deltaTime));






    }


    private void ThirdPerson()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.forward, out hit, thirdPersonCameraDist))
        {
            camT.transform.position = hit.point;
        }
        else
        {
            camT.transform.position = transform.position - transform.forward * thirdPersonCameraDist;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(bounce,1);
    }


}
