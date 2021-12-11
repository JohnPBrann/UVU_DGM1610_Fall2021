using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public GameObject pickupParticle;
    int value = 1;
   [Header("Bobbing Motion")]
    public float rotationSpeed;
    public float bobSpeed;
    public float bobHeight;
    private bool bobbingUp;
    
    private Vector3 startPos;


    void Start()
    {
       startPos = transform.position; 
    }

     void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
                    
            {
                player.GiveHealth(value);
                    
            }
            GameObject obj = Instantiate(pickupParticle, transform.position, Quaternion.identity);
            Destroy(obj, 4f);
            Destroy(gameObject);
            }
        }

       void Update() 
       {
           //Rotates the pickup around the Y-Axis
           transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

           Vector3 offset = (bobbingUp == true ? new Vector3(0, bobHeight / 2, 0) : new Vector3(0, -bobHeight, 0));
          
           transform.position = Vector3.MoveTowards(transform.position, startPos + offset, bobSpeed * Time.deltaTime);

           if(transform.position == startPos + offset)
            bobbingUp = !bobbingUp;
       }
    }
  