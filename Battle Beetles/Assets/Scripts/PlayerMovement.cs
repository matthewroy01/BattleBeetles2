using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [Header("Movement values")]
   public float beetleRotSpd;
   public float moveSpd; 
   public float jumpSpd; 

   // axes to be used later
   float leftStickHoriz;
   float leftStickVert;
   float rightStickHoriz;
   float rightStickVert;

   private Rigidbody rb;

   void Start ()
   {
      rb = GetComponent<Rigidbody>();
   }
	
   void FixedUpdate()
   {
      CheckInput();
   }

   void CheckInput()
   {
      LeftStickInput();
      RightStickInput();
   }

   void LeftStickInput()
   {
      leftStickHoriz = Input.GetAxis("HorizontalLeft");
      leftStickVert = Input.GetAxis("VerticalLeft");

      // **** this needs to be rewritten so that the movement is relative to the camera's Vector3.forward
      rb.velocity = transform.forward * leftStickVert * moveSpd;
      rb.velocity = transform.right * leftStickHoriz * moveSpd;
   }

   void RightStickInput()
   {
      rightStickHoriz = Input.GetAxis("HorizontalRight");

      // rotate horizontally
      transform.Rotate(0, rightStickHoriz * beetleRotSpd, 0);
   }
}
