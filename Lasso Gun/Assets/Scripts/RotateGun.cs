using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public LassoGun lassoGun;
    private float rotationSpeed = 5f;
    private Quaternion desiredRotation;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead == false)
        {
            if(!lassoGun.IsGrappling())
            {
                desiredRotation = transform.parent.rotation;
            }
            else
            {
                desiredRotation = Quaternion.LookRotation(lassoGun.GetGrapplePoint() - transform.position);
            }
            transform.rotation = Quaternion.Lerp(a: transform.rotation, b: desiredRotation, t: Time.deltaTime * rotationSpeed);
        }
    
    }
}
