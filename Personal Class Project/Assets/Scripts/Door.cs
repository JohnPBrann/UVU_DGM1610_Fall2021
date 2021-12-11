using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public bool playerHasKey;
    private bool canOpen;
    public float speed = 5;
    public Key key;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        playerHasKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(key.isHeld == true)
        {
            playerHasKey = true;
        }
        if(canOpen == true)
        {
            if (Input.GetKeyDown("mouse 0"))
            {
                isOpen = true;
            }
        }
        
        if (isOpen == true)
        {
            Vector3 newPos = new Vector3(transform.position.x, -86, transform.position.z);
            transform.position =  Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            {
            if(playerHasKey == true)
                {
                    canOpen = true;
                }
            } 
    }  
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canOpen = false;
        }
    }
}
