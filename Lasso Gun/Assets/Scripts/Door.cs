using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public GameObject door;
    private bool canOpen;
    public float speed = 5;
    private Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = door.transform.position;
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isOpen == true)
        {
            Vector3 newPos = new Vector3(door.transform.position.x, door.transform.position.y + 7, door.transform.position.z);
            door.transform.position =  Vector3.Lerp(door.transform.position, newPos, Time.deltaTime * speed);
        }
         if (isOpen == false)
        {
            door.transform.position =  Vector3.Lerp(door.transform.position, oldPosition, Time.deltaTime * speed);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
            {
                isOpen = true;
            } 
    }  
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            isOpen = false;
        }
    }
}
