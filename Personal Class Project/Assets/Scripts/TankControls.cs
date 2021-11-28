using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls : MonoBehaviour
{

  public GameObject thePlayer;
  public bool isHoldingCamera;
  public float horizontalMove;
  public float verticalMove;
  public GameObject cameraPlacement;
  private PickUpObject pickUpObject;
  


   [Header ("Mouse Look")]
   public GameObject head;

   public float lookSensitivity; //Mouse Look sensitivity
   public float maxLookX; //lowest down we can look
   public float minLookX; //highest we can look
   public float rotX; //Current x rotation
   private Rigidbody rb;

 
   

    [Header("Stats")]
        public float moveSpeed;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
   {
       if(isHoldingCamera == false)
       {
            TankControl();
       }
       else
       {
            Move();
       }
   }
   void TankControl()
    {
      horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 350;
      verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
      thePlayer.transform.Rotate(0, horizontalMove, 0);
      thePlayer.transform.Translate(0, 0, verticalMove);
    }

     void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 dir = transform.right * x +
         transform.forward * z;
        dir.y = rb.velocity.y;
        rb.velocity = dir;
    }

 void HeadLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
        
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        transform.eulerAngles += Vector3.up * y;
        head.transform.localRotation = Quaternion.Euler(-rotX,0,0);
    }

 
   
}