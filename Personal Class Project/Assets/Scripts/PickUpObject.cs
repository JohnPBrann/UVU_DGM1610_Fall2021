using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHead; 
    public bool canPickup; //Checks whether or not the item can be picked up
    public GameObject camObject;
    public bool hasItem; //checks if the item is held
    public new Camera camera;

    [Header("Camera Distance")]
        public float camDistanceX;
        public float camDistanceY;
        public float camDistanceZ;
   
    void Start()
    {
        canPickup = false;
        hasItem = false;
    }

   
    void Update()
    {
        if(canPickup == true) //within the objects collider
        {
            if(Input.GetKeyDown("mouse 0") && hasItem == false) //E key
            { 
                hasItem = true;
            }
            if(hasItem == true)
            {   camObject.GetComponent<BoxCollider>().isTrigger = true;
                camObject.GetComponent<Rigidbody>().isKinematic = true; // rigidbody doesn't get effected by physics
                camObject.transform.position = new Vector3((myHead.transform.position.x), (myHead.transform.position.y + -3), (myHead.transform.position.z));
                camObject.transform.localRotation = myHead.transform.rotation; // rotates object to face same direction as player
                camObject.transform.parent = myHead.transform; // causes object to follow hand location
                camObject.transform.position = new Vector3((myHead.transform.position.x + camDistanceX), (myHead.transform.position.y + camDistanceY), (myHead.transform.position.z + camDistanceZ));
                camera.transform.localRotation = myHead.transform.rotation; // rotates object to face same direction as player
                camera.transform.parent = myHead.transform; // causes object to follow hand location
                
            }
        }

        //old drop script 
       // if (Input.GetKeyDown("mouse 1" ) && hasItem == true)
    //    {
            
           // objectToPickUp.transform.SetParent(null); //disown your child
          //  hasItem = false;
            //objectToPickUp.GetComponent<Rigidbody>().isKinematic = false; // rigidbody activates again
      //  }
    }
    void OnTriggerEnter(Collider other) //checks if player is within collider
    {
        if(other.gameObject.tag == "Camera Object")
        canPickup = true; //player can pick up the object
        camObject = other.gameObject; //set the collided object to be reference
    }
    void OnTriggerExit (Collider other)
    {
        canPickup = false; //player can't pick up object
    }
}
