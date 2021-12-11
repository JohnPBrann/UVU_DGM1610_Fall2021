using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed, attackRange, yPathOffset;
    private List<Vector3> path;
    private Gun gun;
    private GameObject target;
    private bool isDead;
    public List<Transform> items = new List<Transform> ();
    // Start is called before the first frame update
    void Start()
    {

        gun = GetComponent<Gun>();
        target = FindObjectOfType<PlayerController>().gameObject;
        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    void UpdatePath()
    {
         //Calculate a path to the target
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

        path = navMeshPath.corners.ToList();
    }


    void ChaseTarget()
    {
         if(path.Count == 0)
            return;

        //Move towards the closest path
        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0,yPathOffset,0), moveSpeed * Time.deltaTime);

        if(transform.position == path[0] + new Vector3(0, yPathOffset, 0))
            path.RemoveAt(0);    

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angle;
        
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if(dist <= attackRange)
        {
            gun.Shoot();
        }
        else
        {
            ChaseTarget();
        }
        
    }
          public void Die()
    {
        if(isDead == false){
        Debug.Log("Enemy dead");
        isDead = true;
        Destroy(this.gameObject);
        dropHealthpack(); 
        }
         void dropHealthpack() 
    {
        Instantiate(items[Random.Range(0,items.Count-1)],transform.position,Quaternion.identity);
    }
    }
}
