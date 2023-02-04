using UnityEngine;

public class EnhancedDoosLocomotion : MonoBehaviour
{
    // Public variables that can be adjusted from the Unity Editor.
    public float moveSpeed = 6f;   // The speed of the character's movement.
    public float gravity = -9.81f; // The gravity applied to the character's movement.
    public CharacterController controller;   // The character controller component attached to the object.

    // Private variables for internal use.
    private Vector3 moveDirection = Vector3.zero;  // The direction the character is moving.
    [SerializeField]private int health = 100;   // The character's health, serialized to show in the Unity Editor.
    private bool lockedOn = false;  // A flag to determine if the character is locked on to an enemy.

    private bool hasLockIcon = false;   // A flag to determine if the lock icon has been instantiated.

    // Update function, called once per frame.

    void Update()
    {
        // Get input axis for horizontal and vertical movement.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the move direction based on input axis and character's transform.
        moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed * Time.deltaTime;

        // Add gravity to the move direction.
        moveDirection.y = gravity * Time.deltaTime;

        // Move the character based on the calculated move direction.
        controller.Move(moveDirection);

        // If the player presses the left control key, toggle the locked on flag.
        if (Input.GetKeyDown(KeyCode.H))
        {
            lockedOn = !lockedOn;
        }

        LockOn();
    }

    // Function to reduce the character's health by a specified amount.
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Function to reduce the character's health by half of the specified amount when blocked.
    public void TakeBlockedDamage(int damage)
    {
        health -= damage / 2;
    }

void LockOn()
{
    // If the character is locked on to an enemy, look at the closest enemy and disable the character's camera.
    if (lockedOn)
    {
        // Get all the enemies in the scene.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestEnemyDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        // Loop through all the enemies and find the closest one.
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestEnemyDistance)
            {
                closestEnemyDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        // Look at the closest enemy using Lerp.
        transform.LookAt(new Vector3(closestEnemy.transform.position.x, transform.position.y, closestEnemy.transform.position.z));
        GetComponentInChildren<camera>().enabled = false;

        // if the lockOnAnchor isn't a child of the closest enemy, instantiate it.
        if (closestEnemy.transform.Find("lockonAnchor(Clone)") == null) {
            GameObject lockOnAnchor = Instantiate(Resources.Load("lockOnAnchor"), closestEnemy.transform.position, Quaternion.identity) as GameObject;
            lockOnAnchor.transform.parent = closestEnemy.transform;
            lockOnAnchor.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        // If there are multiple lockOnAnchors, destroy the oldest one. Check the count by getting the number of gameObjects with "lockOnAnchor" in their name.
        if (GameObject.FindGameObjectsWithTag("lockOnAnchor").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("lockOnAnchor")[0]);
        }
    }
    // If the character is not locked on to an enemy, enable the character's camera.
    else
    {
        GetComponentInChildren<camera>().enabled = true;

        foreach (GameObject lockOnAnchor in GameObject.FindGameObjectsWithTag("lockOnAnchor"))
        {
            Destroy(lockOnAnchor);
        }
    }
    }
}