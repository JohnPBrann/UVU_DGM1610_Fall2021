using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject thePlayer;
  public bool isHoldingCamera;
  public float horizontalMove;
  public float verticalMove;
  public GameObject cameraPlacement;
  public CameraPickUpSystem pUO;
  public bool isCrosshairVisible;
  public GameObject crosshair;
  GameObject GameManager;


   [Header ("Mouse Look")]
   

   public float lookSensitivity; //Mouse Look sensitivity
   public float maxLookX; //flowest down we can look
   public float minLookX; //highest we can look
   public float rotX; //Current x rotation
   public float rotY;
   private Rigidbody rb;
   public  new Camera camera;

 
   

    [Header("Stats")]
        public float moveSpeed;

    void Awake()
    {
        GameObject GameManager = GameObject.Find("GameManager");
    }
    void Start()
    {
        isCrosshairVisible = false;
       
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
   { 
       if(Time.timeScale == 1)
       {
       CamRotation();
       Crosshair();
       if (isCrosshairVisible == true)
       {
           crosshair.SetActive(true);
       }
       else
       {
           crosshair.SetActive(false);
       }
       

       if(pUO.hasCamera == true)
       {
           isHoldingCamera = true;
       }
       else
       {
           isHoldingCamera = false;
       }
       if(isHoldingCamera == false)
       {
            TankControls();
            CamSlightLook();
       }
       else
       {
           
            Move();
            CamLook();
           
       }
                RaycastHit hit;
                Ray mousePos = camera.ScreenPointToRay(Input.mousePosition);
         if (Physics.Raycast(mousePos, out hit))
             {
                 if(hit.transform.tag ==("Place Point"))
                 {
                     hit.transform.SendMessage ("LookedAt", SendMessageOptions.RequireReceiver);
                 }
                
             }  
       }
       else
       {
           
       }   
   }
   void TankControls()
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

 void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        transform.eulerAngles += Vector3.up * y;
        camera.transform.localRotation = Quaternion.Euler(-rotX,0,0);
    }
    void CamSlightLook()
    {
        float slightSensitivity = 200;
        float lookRestrictionX = 30;
        float lookRestrictionY = 10;

        rotY += Input.GetAxis("Mouse X") * slightSensitivity * Time.deltaTime;
        rotX += Input.GetAxis("Mouse Y") * slightSensitivity * Time.deltaTime;
        
        rotX = Mathf.Clamp(rotX, -lookRestrictionX, lookRestrictionX);
        rotY = Mathf.Clamp(rotY, -lookRestrictionY, lookRestrictionY);
        camera.transform.localRotation = Quaternion.Euler(-rotX, rotY,0);
    }

 void Crosshair()
    { 
        if(isHoldingCamera == true)
        {
            isCrosshairVisible = true;
        }
        else
        {
            isCrosshairVisible = false;
        }
    }


    void CamRotation()
    {
        if(isHoldingCamera == true)
        {
            if (Input.GetKeyDown("q"))
            {
                GameObject ghostCam = GameObject.Find("TempCam(Clone)");
                cameraPlacement.transform.Rotate(0, 15, 0 );
                ghostCam.transform.Rotate(0, 15, 0 );
            }
            if (Input.GetKeyDown("e"))
            {
                GameObject ghostCam = GameObject.Find("TempCam(Clone)");
                cameraPlacement.transform.Rotate(0, -15, 0 );
                ghostCam.transform.Rotate(0, -15, 0 );
            }
         
        }
        if(isHoldingCamera == false)
        {
            if (Input.GetKeyDown("q"))
            {
                 GameObject cam = GameObject.Find("Camera Object");
                 cam.transform.Rotate(0, -15, 0 );
            }
            if (Input.GetKeyDown("e"))
            {
                 GameObject cam = GameObject.Find("Camera Object");
                 cam.transform.Rotate(0, 15, 0 );
            }
        }
         
        
 
    }

     
 }
 
   

