using UnityEngine;

public class EnhancedDoosLocomotion : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float gravity = -9.81f;
    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField]private int health = 100;
    private bool lockedOn = false;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed * Time.deltaTime;
        
        // Add gravity
        moveDirection.y = gravity * Time.deltaTime;
        
        controller.Move(moveDirection);
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

}
