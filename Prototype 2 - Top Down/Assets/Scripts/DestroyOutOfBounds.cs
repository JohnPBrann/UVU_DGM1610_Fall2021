using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
 
    public float topBound = 5.0f;

     public float downBound = -5.0f;

    public float rightBound =  11.0f;

    
    public float leftBound = -11.0f;

    // Update is called once per frame
    void Update()
    {
if(transform.position.y > topBound)

    {
     Destroy(gameObject);
    }
if(transform.position.y < downBound)

    {
     Destroy(gameObject);
    }if(transform.position.x > rightBound)

    {
     Destroy(gameObject);
    }if(transform.position.x < leftBound)

    {
     Destroy(gameObject);
    }

    }
}
