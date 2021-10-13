using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float moveSpeed;  //movement speed in units per second

   public float jumpForce;  //force applied upwards

   public float lookSensitivity; //Mouse Look sensitivity

   public float maxLookX; //lowest down we can look
   
   public float minLookX; //highest we can look

   public float rotX; //Current x rotation

   private Camera camera;
   private Rigidbody rb;
   
    private Weapon weapon;

     void Awake() 
    {
        weapon = GetComponent<Weapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CamLook();
            
            if(Input.GetButton("Fire1"))
        {
            if(weapon.CanShoot())
                weapon.Shoot();
        }

           if(Input.GetButtonDown("Jump"))
            Jump();
        
    }

   void FixedUpdate() 
   {
        

    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        //rb.velocity = new Vector3(x, rb.velocity.y, z);   old code

        Vector3 dir = transform.right * x +
         transform.forward * z;
        dir.y = rb.velocity.y;
        rb.velocity = dir;

    }

    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
        
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        camera.transform.localRotation = Quaternion.Euler(-rotX,0,0);
        transform.eulerAngles += Vector3.up * y;
    }

void Jump()
{
    Ray ray = new Ray(transform.position, Vector3.down);

    if(Physics.Raycast(ray, 1.1f))
    {
    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}

}

