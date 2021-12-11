using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Gun : MonoBehaviour
{
    public bool isLockedUp;
    public Transform muzzle;
    public ObjectPool bulletPool;

    public bool playerHandsFull; 
    public float bulletSpeed;
    public float shootRate;
    private float lastShootTime;
    public bool canPickup;
    public PlayerController playerController;
    public GameObject glassCase;
    public GameObject gun;
    public GameObject player;
    public GameObject gunPlacement;
    public bool isHeld;
    public TextMeshProUGUI messageText; 
    public GameObject message; 
    public bool anyway;


    // Start is called before the first frame update
    void Start()
    {
        isLockedUp = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.isHoldingCamera == true)
        {
            playerHandsFull=true;
        }
        else
        {
            playerHandsFull = false;
        }
        if(Input.GetKeyDown("mouse 0"))
        {
                GunShoot();
        }
       
       if( glassCase == null)
       {
           isLockedUp = false;
       }
       if(canPickup == true)
       {
            if(Input.GetKeyDown("mouse 0"))
            {
                GunPickup();
            }
       }
       
    }
    void GunPickup()
    {
            if(playerHandsFull == false)    
             {
                gun.transform.localRotation = gunPlacement.transform.rotation; // rotates object to face same direction as player
                gun.transform.parent = gunPlacement.transform; // causes object to follow hand location
                gun.transform.position = gunPlacement.transform.position;
                canPickup=false;
                isHeld = true;
             }
             if(playerHandsFull == true)
             {
                
             }
             Message();
    }

    void GunShoot()
    {
        if(isHeld == true)
        {
            if(playerHandsFull==false || anyway == true)
            {
              lastShootTime = Time.time;
              GameObject bullet = bulletPool.GetObject();

              bullet.transform.position = muzzle.position;
              bullet.transform.rotation = muzzle.rotation;

              bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;

            }
        }
    }
    void Message()
    {
        message.SetActive(true);
        messageText.text = isHeld == true ? "Gun collected" : "Silly... your hands are full. You're going to have to set down that camera";
        StartCoroutine(Fade(messageText));
         
    }
     IEnumerator Fade(TextMeshProUGUI messageText)
    {
        Debug.Log("Coroutine started");
        {
            float fadeTime = 1.0f;
            float waitTime = 0;
            while (waitTime < 1)
            {
                messageText.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.black, Color.clear, waitTime));
                yield return null;
                waitTime += Time.deltaTime / fadeTime;
            }
        Debug.Log("Coroutine ended");
        message.SetActive(false);
        }
    }
     void OnTriggerEnter(Collider other) //checks if player is within collider
    {
        if(other.gameObject.tag == "Player")
        {
            if (isLockedUp == false)
            {
                canPickup = true; //player can pick up the object
        }
        }
    }
     void OnTriggerStay(Collider other) //checks if player is within collider
    {
        if(other.gameObject.tag == "Player")
        {
            if (isLockedUp == false)
            {
                canPickup = true; //player can pick up the object
        }
        }
    }
    void OnTriggerExit (Collider other)
    {
         if(other.gameObject.tag == "Player")
        {
            canPickup = false; //player can pick up the object
        }
    }
}
