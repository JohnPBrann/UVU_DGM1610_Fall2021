using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ObjectPool bulletPool;  // <--- (new code)
    public Transform muzzle;

    public float bulletSpeed;

    public float shootRate;

    private float lastShootTime;

    [Header ("SFX")]
        private AudioSource audioSource;
        public AudioClip shootSFX;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool CanShoot()
    {
         if(Time.time - lastShootTime >= shootRate)
         {
            return true;
         }

        return false;
    }
    
    public void Shoot()
    {
        
        if(CanShoot()==true)
        {
            audioSource.PlayOneShot(shootSFX);
            lastShootTime = Time.time;
            GameObject bullet = bulletPool.GetObject();
            bullet.transform.position = muzzle.position;
            bullet.transform.rotation = muzzle.rotation;
            bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;
        }
    }
}
