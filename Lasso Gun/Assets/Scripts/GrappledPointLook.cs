using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappledPointLook : MonoBehaviour
{
     private Transform target;  
     public bool enemyHooked;
     public GameObject hookedEnemy;
     public bool grappledPointReturn;
     private LassoGun lassoGun;
     
    // Start is called before the first frame update
    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lassoGun = GameObject.FindGameObjectWithTag("Player").GetComponent<LassoGun>();
    }

     void Update()
    {
        if(hookedEnemy == null)
        {
            enemyHooked = false;
        }
        
        //Look at the target
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angle;
        if(enemyHooked==true)
        {
            hookedEnemy.transform.parent = this.transform;
            grappledPointReturn = false;
            if(Input.GetButtonUp("Fire1"))
            {
                hookedEnemy.transform.parent = null;
                enemyHooked = false;
                grappledPointReturn = true;
            }
        }
        
        if(grappledPointReturn==true)
        {
            Vector3 playerPos = target.transform.position;
            this.transform.position = Vector3.Lerp(a: this.transform.position, b: playerPos, 1 * Time.deltaTime);
        }
        if (lassoGun.IsGrappling()==true)
        {
            grappledPointReturn = false; 
        }
    }
    public void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Enemy" )
        {
            enemyHooked = true;
            hookedEnemy = other.gameObject;
            
        }
    }
    public void OnTriggerExit (Collider other)
    { 
        
        enemyHooked = false;
        hookedEnemy = null;
        lassoGun.StopGrapple();
    }

    
}
