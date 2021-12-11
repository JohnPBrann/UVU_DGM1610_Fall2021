using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
   public GameObject glassCase;
   
   void Start()
   {
       
   }
   
   void OnTriggerEnter(Collider other)
   {
        if(other.gameObject.tag == "Bullet")
        {
               Destroy (this.gameObject);
        }      
   }
   
}
