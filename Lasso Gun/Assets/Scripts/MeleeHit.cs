using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    private LassoGun lassoGun;
    // Start is called before the first frame update
    void Start()
    {
      lassoGun = FindObjectOfType<LassoGun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(lassoGun.isSwinging == true)
        {
            if(other.gameObject.tag == "Enemy")
            {
                other.GetComponent<Enemy>().Die();
            }
            if(other.gameObject.tag == "MobileEnemy")
            {
                other.GetComponent<EnemyMove>().Die();
            }
        }
    }
}
