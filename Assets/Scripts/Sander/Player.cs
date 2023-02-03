using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //fields for the camera
    [SerializeField]private Camera playerCamera;

    //fields for the movement
    private float speed = 12f;
    private float deccel = 7f;
    private float jumpForce = 10f;
    private float jumpHeight = 0;
    private float gravity = 9.81f;

    [SerializeField]private int health = 100;

    private bool floored;
    private bool colliding;
    private bool wall;
    public bool moveEnabled = true;
    private bool lockedOn = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveEnabled)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                if(!lockedOn)
                {
                    lockedOn = true;
                }
                else
                {
                    lockedOn = false;
                }
            }
            if(speed > 12f && floored)
            {
                speed -= deccel * Time.deltaTime;
            }
            Move();
            Jump();
            LockOn();
            if (jumpHeight > 0)
            {
                transform.position += Vector3.up * jumpHeight * Time.deltaTime;
                jumpHeight -= gravity * Time.deltaTime;
            }
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if(colliding && speed > 9f)
        {
            speed -= deccel * 200 * Time.deltaTime;
        }
        else if(!colliding && speed < 12f)
        {
            speed = 12f;
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(floored)
            {
                jumpHeight = jumpForce;
                transform.position += Vector3.up * jumpHeight * Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void TakeBlockedDamage(int damage)
    {
        health -= damage / 2;
    }

    void LockOn()
    {
        if(lockedOn)
        {
            transform.LookAt(GetComponentInChildren<Melee>().Enemy);
            GetComponentInChildren<camera>().enabled = false;
        }
        else
        {
            GetComponentInChildren<camera>().enabled = true;
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Floor")
        {
            floored = true;
        }
        if(collisionInfo.collider.tag == "Wall")
        {
            colliding = true;
            wall = true;
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Floor")
        {
            floored = false;
        }
        if (collisionInfo.collider.tag == "Wall")
        {
            colliding = false;
            wall = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Axe")
        {
            TakeDamage(10);
        }
    }
}
