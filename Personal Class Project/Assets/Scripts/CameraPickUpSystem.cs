using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPickUpSystem : MonoBehaviour
{
    public GameObject player; 
    public bool canPickup; //Checks whether or not the item can be picked up
    public bool hasCamera; //checks if the camera is held
    public bool playerCanPlaceHere; //checks if the surface is identified as somewhere the camera can be placed
    public new Camera camera;
    public Transform camPlacement;
    public GameObject camObject;
    public GameObject camObjectPrefab;
    public Transform mouseTarget;
    public GameObject TempCam;
    private GameObject tempObj;
    private GameObject newObj;
    private bool isGhostCreated;
    public Transform activePlacePoint;

    void Start()
    {
        playerCanPlaceHere = false;
        canPickup = false;
        hasCamera = false;
        isGhostCreated = false;
        
    }
    
    void Update()
    {
        
         if(Time.timeScale == 1)
       {
        Ray mousePos = camera.ScreenPointToRay(Input.mousePosition);
        if(hasCamera == (false))
        {
            PickUpCamera();
        }
        if(hasCamera == (true))
        {
        PlaceCamera();
        }
       }
       else{}
    }

    void PickUpCamera()
    {
       {

       if(canPickup == true) //within the objects collider
        {if(hasCamera == (false))
         {
            if(Input.GetKeyDown("mouse 0") && hasCamera == false) 
            { 
                hasCamera = true;
            }
            if(hasCamera == true)
            {
              GameObject camObject = GameObject.Find("Camera Object");
              camObject.GetComponent<BoxCollider>().isTrigger = true;
              camObject.GetComponent<Rigidbody>().isKinematic = true; // rigidbody doesn't get effected by physics
              camObject.transform.position = new Vector3((player.transform.position.x), (player.transform.position.y), (player.transform.position.z));
              camObject.transform.localRotation = player.transform.rotation; // rotates object to face same direction as player
              //camObject.transform.position = new Vector3((player.transform.position.x + camDistanceX), (player.transform.position.y + camDistanceY), (player.transform.position.z + camDistanceZ));
               // Destroy(camObject);

                camera.transform.localRotation = player.transform.rotation; // rotates object to face same direction as player
                camera.transform.parent = player.transform; // causes object to follow hand location
                Destroy(camObject);
                PlacePointController();
                canPickup=false;
            }
        }
        }
       
       }


    }

    void PlaceCamera()
    {
        RaycastHit hit;
        Ray mousePos = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mousePos, out hit))
        {
               GhostCam();
            if(hit.transform.tag == "Place Point")
            {   
                if(Input.GetKeyDown("mouse 1"))
                {
                   OnRightClick();
                }
        }   

    }
 

    void OnRightClick()
    {
     GameObject newObj = Instantiate(camObjectPrefab, camPlacement.position, camPlacement.rotation); 
     camera.transform.rotation = newObj.transform.rotation;
     camera.transform.position = (new Vector3(newObj.transform.position.x, newObj.transform.position.y, newObj.transform.position.z));
     camera.transform.parent = newObj.transform;
     newObj.transform.parent = activePlacePoint;
     hasCamera = false;
     newObj.name = ("Camera Object");
    }

    void GhostCam()
    { 
        if(hit.transform.tag == "Place Point")
        {   
         if(isGhostCreated == false)
             {
              GameObject ghostCam = Instantiate(TempCam, camPlacement.position, camPlacement.rotation);
              ghostCam.transform.parent = activePlacePoint; 
              isGhostCreated = true;
             }
        }
                else
            {
                GameObject ghostCam = GameObject.Find("TempCam(Clone)");
                Destroy(ghostCam);
                isGhostCreated = false;
            }
    }

   }
   void OnTriggerEnter(Collider other) //checks if player is within collider
    {
        if(other.gameObject.tag == "Camera Object")
        {
            canPickup = true; //player can pick up the object
            //camObject = other.gameObject; //set the collided object to be reference
        }
    }
    void OnTriggerExit (Collider other)
    {
         if(other.gameObject.tag == "Camera Object")
        {
            canPickup = false; //player can pick up the object
            //camObject = other.gameObject; //set the collided object to be reference
        }
    }

    void PlacePointController()
    {
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

}
