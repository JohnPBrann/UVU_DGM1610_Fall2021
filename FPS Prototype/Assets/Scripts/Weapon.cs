using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   // public GameObject bulletPrefab; (old code)
   public ObjectPool bulletPool;  // <--- (new code)
    public Transform muzzle;

    public int curAmmo;
    public int maxAmmo;
    public bool infiniteAmmo;

    public float bulletSpeed;

    public float shootRate;

    private float lastShootTime;
    private bool isPlayer;


     void Awake() 
     {
         //Disable Cursor
         Cursor.lockState = CursorLockMode.Locked;

         if(GetComponent<PlayerController>())
            isPlayer = true;
    }

    public bool CanShoot()
    {
         if(Time.time - lastShootTime >= shootRate)
         {
            if(curAmmo > 0 || infiniteAmmo == true)
            return true;
        }

        return false;
    
    }

    public void Shoot()
    {
        // Cooldown
        lastShootTime = Time.time;
        curAmmo --;

      //  Creating an instance of the bullet prefab at muzzles position and rotation
      //  GameObject  bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        GameObject bullet = bulletPool.GetObject();

        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;

        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;
        if(isPlayer)
        {
            GameUI.instance.UpdateAmmoText(curAmmo, maxAmmo);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
