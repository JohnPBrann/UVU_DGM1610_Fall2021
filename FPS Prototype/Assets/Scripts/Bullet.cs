using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float lifetime;
    private float shootTime;

    public GameObject hitParticle;

    void OnEnable() 
        {
            shootTime = Time.time;
        }
    void OnTriggerEnter(Collider other) 
    {
        //If hit deal out damage to the player

        if(other.CompareTag("Player"))
            other.GetComponent<PlayerController>().TakeDamage(damage);

            //If hit deals damage to the enemy

            else if(other.CompareTag("Enemy"))
                other.GetComponent<Enemy>().TakeDamage(damage);
            //Disable Projectile for furute use
            gameObject.SetActive(false);
            // Create the hit particle 
            GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(obj, 1f);
    }

    //Update is called once per frame
    void Update()
    {
        if(Time.time - shootTime >= lifetime)
            gameObject.SetActive(false);

    }
}