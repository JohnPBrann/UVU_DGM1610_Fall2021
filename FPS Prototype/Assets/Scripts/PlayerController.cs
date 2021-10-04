using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float moveSpeed;  //movement speed in units per second

   public float turnSpeed;

   public float jumpForce;  //force applied upwards

   public float lookSensitivity; //Mouse Look sensitivity

   public float maxLookX; //lowest down we can look
   
   public float minLookX; //highest we can look

   public float rotX; //Current x rotation

   private Camera camera;
   private Rigidbody rb;
   


    // Start is called before the first frame update
    void Start()
    {
        camera = camera.main;
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float y = Input.GetAxis("Vertical") * moveSpeed;

        rb.velocity = new Vector3(x, rb.Velocity.y, z);

    }

    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
    }
}

