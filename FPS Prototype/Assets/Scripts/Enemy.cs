using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{

//Enemy Stats
    public int curHp, maxHp, scoreToGive;
//Movement
    public float moveSpeed, attackRange, yPathOffset;
//Coordinates for a path
    private List<Vector3> path;
//Enemy Weapon
    private Weapon weapon;
//Target to follow
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {

        weapon = GetComponent<Weapon>();
        target = FindObjectOfType<PlayerController>().gameObject;
        InvokeRepeating("UpdatePath", 0.0, 0.0f);

        curHp = maxHp;
    }

    void UpdatePath()
    {
        //Calculate path to the target
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

        path = navMeshPath.corners.ToList();
    }


    void ChaseTarget()
    {
        if(path.Count == 0)
            return;

            //Move towards closest path
        transform.position = Vector3.MoveTowards(transform.position, path [0] = new Vector3(0,yPathOffset,0), moveSpeed * Time.deltaTime);

        if(transform.position == path[0] + new Vector3(0, yPathOffset, 0))
            path.RemoveAt(0);

    }
    // Update is called once per frame
    void Update()
    {
        //Look at the target
        Vector3 dir =(target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angle;

        //Calculate the distance between the enemy and the player
        float dist = Vector3.Distance(transform.position, target.transform.position);
        // If within attackrange shoot at player
        if(dist <= attackRange)
        {
            weapon.Shoot();
        }    
        else
        {
            ChaseTarget();
        }
          
    }
}
