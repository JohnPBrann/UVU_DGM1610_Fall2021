using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
[Header ("Movement")]
    public float moveSpeed = 4.5f;
[Header ("Jumping")]
    public float jumpForce;

    [Header ("Stats")]
    public bool isDead;
    public int curHp;
    public int maxHp;

[Header ("Camera Settings")]
    public float lookSensitivity=50;
    private float maxLookX = 75;
    private float minLookX;
    private float rotX;
    public Camera cam;


    private Rigidbody rb;
    private LassoGun lassoGun;

    void Awake()
    {
        lassoGun = GetComponent<LassoGun>();
    }
    // Start is called before the first frame update
    void Start()
    {
        minLookX = -maxLookX;
        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        isDead = false;
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false)
        {
            Move();
            CamLook();
            if(lassoGun.IsGrappling() == false)
            {
                if(Input.GetButtonDown("Jump"))
                {
                       Jump();
                }
            }
        }

    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        //rb.velocity = new Vector3(x, rb.velocity.y, z);   old code

        Vector3 dir = transform.right * x +
         transform.forward * z;
        dir.y = rb.velocity.y;
        rb.velocity = dir;

    }

    void CamLook()
    {
        if(Time.timeScale == 1.0)
        {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
        
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        cam.transform.localRotation = Quaternion.Euler(-rotX,0,0);
        transform.eulerAngles += Vector3.up * y;
        }

    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if(Physics.Raycast(ray, 1.1f))
        {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;

        if(curHp <= 0)
            Die();
    }
    public void GiveHealth(int amountToGive)
   {
        curHp = Mathf.Clamp(curHp + amountToGive, 0, maxHp);
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
   }

    public void Die()
    {
        Debug.Log("You died");
        rb.constraints = RigidbodyConstraints.None;
        GameObject gun = cam.transform.GetChild(0).gameObject;
        gun.transform.parent = null;
        isDead = true;
        lassoGun.enabled = false;
    }
}
