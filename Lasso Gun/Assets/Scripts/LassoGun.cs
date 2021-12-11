using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoGun : MonoBehaviour
{
    public Transform muzzle, cam, player, grappledPoint, gunPos, gunPosition, aimPos;
    public GameObject lassoGun;
    public GrappledPointLook gPL;
    public PlayerController playerController;
    public bool canShoot;

    public float lassoSpeed, reelSpeed;
    private float lastShootTime;
    public GameObject shootParticle;
    private Vector3 grapplePointPosition;
    public Transform grapplePoint;
    public LayerMask whatIsGrapplable;
    public LayerMask whatIsntGrapplable;
    private float maxDistance = 50f;
    private SpringJoint joint;

    private bool isAiming;
    public bool enemyHooked;
    public bool shootMode;
    public bool isSwinging;

    [Header ("SFX")]
    private AudioSource audioSource;
    public AudioClip startGrappleSFX;
    public AudioClip reloadSFX;
    public AudioClip shootSFX;
    
    [Header ("Shotgun Mode Settings")]
    public ObjectPool bulletPool;
    public float bulletSpeed;
   
    void Awake()
    {
       shootMode = true;
       audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        isAiming = false;
        enemyHooked = false;
    }
    void Update()
    {
        if(shootMode == true)
        {
        if(gPL.enemyHooked == true)
        {
            enemyHooked= true;
        }
        else
        {
            enemyHooked = false;
        }
        if(IsGrappling()== false)
        {
            if (Input.GetButton("Fire2"))
                {
                    Aim();
                    canShoot = true;       
                } 

            else
                {
                    StopAim();
                    canShoot = false;
                }  
        }
        if(IsGrappling() == true)
        {
            
             StopAim();
            if (Input.GetButton("Fire1"))
            {    
                 if(Input.GetButton("Jump"))
                 {
                     if(enemyHooked==true)
                     {
                         LassoPull();
                     }
                    else
                    {
                        Vector3 desiredPos = grappledPoint.transform.position;
                        player.transform.position = Vector3.Lerp(a: player.transform.position, b: desiredPos, (reelSpeed +4) * Time.deltaTime);
                    }
                        
                 }
                
                 if(Input.GetButtonUp("Jump"))
                 {
                     Vector3 playerPos = player.transform.position;
                     grappledPoint.transform.position = Vector3.Lerp(a: grappledPoint.transform.position, b: playerPos, reelSpeed * Time.deltaTime);
                 }
            }
        }
        if(canShoot == true)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                StartGrapple();
                audioSource.PlayOneShot(startGrappleSFX, .5f);
                
            }
        }

         if(Input.GetButtonUp("Fire1"))
            {
                StopGrapple();
            }

        Swing();
        }
        if(shootMode==false)
        {

            if (Input.GetButton("Fire2"))
                {
                    Aim();
                    canShoot = true;       
                } 

            else
                {
                    StopAim();
                    canShoot = false;
                }  

            if(canShoot == true)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }
            }
            Swing();
        }

            SwitchShootMode();
    }

    


    public void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(origin: cam.position, direction: cam.forward, out hit, maxDistance, whatIsGrapplable))
        {
            Debug.Log(hit.transform.name);
 
            grapplePoint.position = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint.position;

            float distanceFromPoint = Vector3.Distance(a: player.position, b: grapplePoint.position);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;


            grappledPoint.transform.position = grapplePoint.position;

        }
        if(Physics.Raycast(origin: cam.position, direction: cam.forward, out hit, maxDistance, whatIsntGrapplable))
        {


        }
    }

    void Aim()
    {
        isAiming = true;
        Vector3 aimPosition = aimPos.transform.position;
        Quaternion aimRotation = aimPos.transform.rotation;
        gunPos.transform.position = Vector3.Lerp(a: gunPos.transform.position, b: aimPosition, t: 6 * Time.deltaTime);
        gunPos.transform.rotation = Quaternion.Lerp(a: gunPos.transform.rotation, b: aimRotation, t: 6 * Time.deltaTime);
        playerController.cam.fieldOfView = Mathf.Lerp(playerController.cam.fieldOfView, 45, Time.deltaTime * 3);
        
    }
     public void StopAim()
    {  
        isAiming = false;
       Vector3 originalPosition = gunPosition.transform.position;
       Quaternion originalRotation = gunPosition.transform.rotation;
        gunPos.transform.position = Vector3.Lerp(a: gunPos.transform.position, b: originalPosition, t: 6 * Time.deltaTime);
        gunPos.transform.rotation = Quaternion.Lerp(a: gunPos.transform.rotation, b: originalRotation, t: 6 * Time.deltaTime);
        playerController.cam.fieldOfView = Mathf.Lerp(playerController.cam.fieldOfView, 60, Time.deltaTime * 5);
    }

    public void StopGrapple()
    {
        Destroy(joint);
    }

    void LassoPull()
    {
        
            Debug.Log("Reeling in!"); 
            Vector3 playerPos = player.transform.position;
            grappledPoint.transform.position = Vector3.Lerp(a: grappledPoint.transform.position, b: playerPos, reelSpeed * Time.deltaTime);
    
       // else
       // {
        //    enemyHooked = false;
       //     Vector3 desiredPos = grappledPoint.transform.position;
        //    player.transform.position = Vector3.Lerp(a: player.transform.position, b: desiredPos, (reelSpeed +4) * Time.deltaTime);
        //}
    }

    void Swing()
    {
        if(IsGrappling() == false)
        {
            if(isAiming == false)
            {
              if(Input.GetButtonDown("Fire1"))
                {
                  Animator anim = lassoGun.GetComponent<Animator>();
                 anim.SetBool("Swinging", true);
                 isSwinging = true;
                }
             if(Input.GetButtonUp("Fire1"))
                {
             Animator anim = lassoGun.GetComponent<Animator>();
                if(anim != null)
                    {
                        anim.SetBool("Swinging", false);
                        isSwinging = false;
                    }
                }
            }
        
        }
    }
   public void Shoot()
   {
       audioSource.PlayOneShot(shootSFX);
        GameObject bullet = bulletPool.GetObject();
        lastShootTime = Time.time;
        bullet.transform.position = new Vector3 (muzzle.position.x ,muzzle.position.y ,muzzle.position.z);
        bullet.transform.rotation = muzzle.rotation;
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed; 
   }

  void  SwitchShootMode()
  {
    if(Input.GetButtonDown("Fire3"))
        {
            audioSource.PlayOneShot(reloadSFX);
            Animator anim = lassoGun.GetComponent<Animator>();
            anim.Play("Reload");
        }
    if(Input.GetButtonUp("Fire3"))
        {
            shootMode = !shootMode;
        }
    
  }
    

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grappledPoint.position;
    }
}
