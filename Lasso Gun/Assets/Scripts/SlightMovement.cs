using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlightMovement : MonoBehaviour
{
     [Header("Bobbing Motion")]
    public float rotationSpeed;
    public float bobSpeed;
    public float bobHeight;
    private bool bobbingUp;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; 
    }

    // Update is called once per frame
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
