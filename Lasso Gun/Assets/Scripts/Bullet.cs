using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float lifetime;
    private float shootTime;
    public GameObject hitParticle;
    [Header ("SFX")]
        private AudioSource audioSource;
        public AudioClip impactSFX;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable() 
        {
            shootTime = Time.time;
        }
    void OnTriggerEnter(Collider other) 
    {
        //If hit deal out damage to the player

        if(other.transform.tag =="Player")
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
            gameObject.SetActive(false);
            // Create the hit particle 
            audioSource.PlayOneShot(impactSFX);
            GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(obj, 1f);
        }
         if(other.CompareTag("Enemy"))
         {
           other.GetComponent<Enemy>().Die();
    
            //Disable Projectile for future use
            gameObject.SetActive(false);
            
           
         }
          if(other.CompareTag("MobileEnemy"))
         {
           other.GetComponent<EnemyMove>().Die();
           
            //Disable Projectile for future use
            gameObject.SetActive(false);
            
            particle();;
         }
         if(other.transform.tag =="Door")
         {
             
            //Disable Projectile for future use
            gameObject.SetActive(false);
            // Create the hit particle 
            
            particle();
         }
    }

    //Update is called once per frame
    void Update()
    {
        if(Time.time - shootTime >= lifetime)
            {
            gameObject.SetActive(false);
            particle();
            
            }

    }

    
    void particle()
    {
         // Create the hit particle 
            GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(obj, 1f);
    }
}