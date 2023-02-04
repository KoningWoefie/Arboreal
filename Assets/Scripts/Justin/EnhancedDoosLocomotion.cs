using UnityEngine;
using UnityEngine.UI;

public class EnhancedDoosLocomotion : MonoBehaviour
{
    // Public variables that can be adjusted from the Unity Editor.
    public float moveSpeed = 6f;   // The speed of the character's movement.
    public float gravity = -9.81f; // The gravity applied to the character's movement.
    public CharacterController controller;   // The character controller component attached to the object.
    [SerializeField]private Timer staminaTimer;
    [SerializeField]private Timer dashCooldownTimer;
    [SerializeField]private Timer staminaRegenTimer;

    // Private variables for internal use.
    private Vector3 moveDirection = Vector3.zero;  // The direction the character is moving.
    [SerializeField] private int health = 100;   // The character's health, serialized to show in the Unity Editor.
    private int maxHealth = 100;    // The character's maximum health.
    [SerializeField] private int stamina = 100;  // The character's stamina, serialized to show in the Unity Editor.
    public int Stamina { get { return stamina; } set { stamina = value; } }
    private int maxStamina = 100;   // The character's maximum stamina.
    private bool lockedOn = false;  // A flag to determine if the character is locked on to an enemy.

    private bool hasLockIcon = false;   // A flag to determine if the lock icon has been instantiated.

    [SerializeField] private GameObject healthBar;    // The health bar that shows the characters health.
    [SerializeField] private GameObject staminaBar;    // The stamina bar that shows the characters stamina.

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
        if(stamina < maxStamina)
        {
            if(staminaTimer.Seconds() == 0)
            {
                staminaTimer.StartTimer();
            }
            else
            {
                staminaTimer.StopTimer();
                staminaTimer.StartTimer();
            }
        }
        if(staminaTimer.Seconds() >= 0.7f)
        {
            if(stamina != maxStamina)
            {
                if(staminaRegenTimer.Seconds() == 0)
                {
                    staminaRegenTimer.StartTimer();
                }
                if(staminaRegenTimer.Seconds() >= 0.05f)
                {
                    stamina++;
                    staminaRegenTimer.StopTimer();
                }
            }
            else
            {
                staminaTimer.StopTimer();
            }
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

    void Dash()
    {
        if (stamina > 0)
        {
            stamina -= 10;
            if(Input.GetKey(KeyCode.A))
            {
                controller.Move(-transform.right * 10);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                controller.Move(transform.right * 10);
            }
            else if(Input.GetKey(KeyCode.W))
            {
                controller.Move(transform.forward * 10);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                controller.Move(-transform.forward * 10);
            }
        }
    }

    void updateHealthBar()
    {
        if(health != 0)
        {
            healthBar.transform.localScale = new Vector3(health / maxHealth, 1, 1);
        }
    }

    void updateStaminaBar()
    {
        if(stamina != 0)
        {
            staminaBar.transform.localScale = new Vector3(stamina / maxStamina, 1, 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Axe")
        {
            TakeDamage(10);
            other.gameObject.transform.parent.gameObject.GetComponent<Axe>().enabled = false;
            other.gameObject.transform.parent.gameObject.transform.parent = transform;
        }
    }
}