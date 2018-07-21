using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovement : MonoBehaviour {


    public float BeetleRotSpd;
    public float CameraRotSpd; 
    public float moveSpd; 
    public float jumpSpd; 
    public Rigidbody rb;

    //Axis Variables
    float leftStickHoriz;
    float leftStickVert;
    float rightStickHoriz;
    float rightStickVert;

    public Camera BeetleCam;
    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        checkInput();
    }

    void checkInput()
    {
        leftStickInput();
        rightStickInput();
        //transform.position = rb.gameObject.transform.position;


        
    }

    void leftStickInput()
    {
        leftStickHoriz = Input.GetAxis("Horizontal");
        leftStickVert = Input.GetAxis("Vertical");

       // Vector3 movement = new Vector3(leftStickHoriz, 0.0f, leftStickVert);

        rb.velocity = transform.forward * leftStickVert * moveSpd;
        rb.velocity = transform.right * leftStickHoriz * moveSpd;
        
    }

    void rightStickInput()
    {
        if(Input.GetKey(KeyCode.I))
        {
            rightStickHoriz = 1;
        }
        else if(Input.GetKey(KeyCode.O))
        {
            rightStickHoriz = -1;
        }
        else
        {
            rightStickHoriz = 0;
        }

        if (Input.GetKey(KeyCode.K))
        {
            rightStickVert = 1;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            rightStickVert = -1;
        }
        else
        {
            rightStickVert = 0;
        }



        //Rotation of the beetle
        transform.Rotate(0, rightStickHoriz * BeetleRotSpd, 0);
       

        //RotateCamera
        BeetleCam.transform.Rotate(rightStickVert * CameraRotSpd, 0.0f, 0.0f);

    }
}
