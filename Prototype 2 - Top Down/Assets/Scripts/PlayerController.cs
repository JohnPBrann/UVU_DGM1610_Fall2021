using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 20.0f;
     public float turnSpeed = 60.0f;
    public float hInput;
    public float vInput;
    public Transform launcher;
    public float xRange = 8.72f;

    public float yRange = 4.95f;

    public GameObject projectile;

    //public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal"); 
        vInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.forward * -turnSpeed * hInput * Time.deltaTime);
          transform.Translate(Vector3.up * speed * vInput * Time.deltaTime);
//Create a wall on the -x side
        if(transform.position.x < -xRange)
        {
transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
  //Create a wall on the x side
          if(transform.position.x > xRange)
        {
transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        //Create a wall on the -y side
        if(transform.position.y < -yRange)
        {
transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }
  //Create a wall on the y side
          if(transform.position.y > yRange)
        {
transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
{
        if(Input.GetButtonDown("Fire1"))

{
Instantiate(projectile, launcher.transform.position, launcher.transform.rotation);        
}
}

    }
}
