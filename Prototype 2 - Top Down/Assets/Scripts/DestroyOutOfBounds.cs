using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
 
    public float topBound = 5.0f;

    // Update is called once per frame
    void Update()
    {
if(transform.position.y > topBound)

    {
     Destroy(gameObject);
    }


    }
}
