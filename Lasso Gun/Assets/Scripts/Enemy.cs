using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{


    public float attackRange;
    private Gun gun;
    private GameObject target;
    private Transform gunPosition;
    private Rigidbody rb;
    private bool isDead;
     public List<Transform> items = new List<Transform> ();

    void Start()
    {
        isDead = false;
        gun = GetComponent<Gun>();
        gunPosition = transform.GetChild(0);
        target = FindObjectOfType<PlayerController>().gameObject;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false){

        Vector3 dir =(target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angle;
        //Aim at player
        //Vector3 gunDir =(target.transform.position - gunPosition.transform.position).normalized;
      //  float gunAngle = Mathf.Atan2(gunDir.x, gunDir.z) * Mathf.Rad2Deg;
       // gunPosition.transform.eulerAngles = Vector3.up * gunAngle;




        float dist = Vector3.Distance(transform.position, target.transform.position);
        // If within attackrange shoot at player
        if(dist <= attackRange)
        {
            gun.Shoot();
        }  
        }  
          
    }
      public void Die()
    {
        if(isDead == false){
        Debug.Log("Enemy Die");
        rb.constraints = RigidbodyConstraints.None;
        isDead = true;
        Destroy(this.gameObject);
        dropHealthpack(); 
        }
    }
   
 
    void dropHealthpack() 
    {
        Instantiate(items[Random.Range(0,items.Count-1)],transform.position,Quaternion.identity);
    }
}
