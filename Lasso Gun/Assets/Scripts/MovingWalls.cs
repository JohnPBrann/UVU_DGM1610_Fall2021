using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    public bool isMoving = false;
    public float speed;
    public Vector3 desiredPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == true)
        {
            transform.position =  Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isMoving = true;
        }
    }
}
