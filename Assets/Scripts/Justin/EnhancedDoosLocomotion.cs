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
    [SerializeField]private Timer hitCooldownTimer;

    // Private variables for internal use.
    private Vector3 moveDirection = Vector3.zero;  // The direction the character is moving.

    [SerializeField] private float health = 100;   // The character's health, serialized to show in the Unity Editor.
    private float maxHealth = 100;    // The character's maximum health.
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    [SerializeField] private float stamina = 100;  // The character's stamina, serialized to show in the Unity Editor.
    public float Stamina { get { return stamina; } set { stamina = value; } }
    private float maxStamina = 100;   // The character's maximum stamina.
    public float MaxStamina { get { return maxStamina; } set { maxStamina = value; } }

    private bool lockedOn = false;  // A flag to determine if the character is locked on to an enemy.

    private bool hasLockIcon = false;   // A flag to determine if the lock icon has been instantiated.

    private bool dashed = false;    // A flag to determine if the character has dashed.

    private bool hit = false;   // A flag to determine if the character has been hit.

    private bool boss = false;   // A flag to determine if the character is fighting a boss.
    private bool melee = true; // A flag to determine if the character is fighting a melee enemy.
    private bool ranged = false;    // A flag to determine if the character is fighting a ranged enemy.

    [SerializeField] private GameObject healthBar;    // The health bar that shows the characters health.
    [SerializeField] private GameObject staminaBar;    // The stamina bar that shows the characters stamina.

    private GameObject axe;

    [SerializeField] private GameObject checkPointHandler;    // The checkpoint handler that handles the checkpoints.
    private GameObject enemy;   // The enemy.
    private GameObject walkAudioHandler;    // The audio handler.

    // Update function, called once per frame.

    void Update()
    {
        // if player presses P, die
        if (Input.GetKeyDown(KeyCode.P))
        {
            health = 0;
        }
        Die();


        // Get input axis for horizontal and vertical movement.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the move direction based on input axis and character's transform.
        moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed * Time.deltaTime;

        if(moveDirection != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("Walking", true);
            // Walking audio
            walkAudioHandler = GameObject.Find("WalkAudioHandler");

            // Play a random audio source from the audioHandler object.
            walkAudioHandler.GetComponent<audioHandlerScript>().PlayRandomAudioSource();
            
        }
        else
        {
            GetComponent<Animator>().SetBool("Walking", false);
        }

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
            else if(dashed || GetComponentInChildren<Melee>().Attacking)
            {
                staminaTimer.StopTimer();
                staminaTimer.StartTimer();
            }
        }
        if(staminaTimer.Seconds() >= 1f)
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
                Debug.Log("Stamina is full");
                staminaTimer.StopTimer();
            }
        }
        if(hitCooldownTimer.Seconds() >= 2f)
        {
            hit = false;
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
            Debug.Log("Hit cooldown over");
            hitCooldownTimer.StopTimer();
            Debug.Log("Hit cooldown over");
        }

        LockOn();
        updateHealthBar();
        updateStaminaBar();
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
        if (stamina >= 10)
        {
            stamina -= 10;
            dashed = true;
            dashCooldownTimer.StartTimer();
            if(Input.GetKey(KeyCode.A))
            {
                controller.Move(-transform.right * 5);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                controller.Move(transform.right * 5);
            }
            else if(Input.GetKey(KeyCode.W))
            {
                controller.Move(transform.forward * 5);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                controller.Move(-transform.forward * 5);
            }
        }
        if(dashCooldownTimer.Seconds() >= 0.5f)
        {
            dashed = false;
            dashCooldownTimer.StopTimer();
        }
    }

    public void Die()
    {
        if(health <= 0)
        {
            GetComponent<soilCurrency>().resetSoils();
            health = maxHealth;
            checkPointHandler.GetComponent<checkpointHandler>().GoToCheckpoint();
        }
    }

    void updateHealthBar()
    {
        if(health != 0)
        {
            healthBar.transform.localScale = new Vector3((float)(health / maxHealth), 1, 1);
        }
    }

    void updateStaminaBar()
    {
        if(stamina != maxStamina)
        {
            staminaBar.transform.localScale = new Vector3((float)(stamina / maxStamina), 1, 1);
        }
    }

    void getHit()
    {
        if(hitCooldownTimer.Seconds() == 0)
        {
            TakeDamage(3);
            if(ranged)
            {
                try{
                axe.GetComponent<Axe>().enabled = false;
                axe.transform.parent = transform;
                }catch{}
            }
            // Enables AttackCooldown.
            if(melee)
            {
                try
                {
                    MeleeLumberJack axe = enemy.gameObject.transform.parent.GetComponent<MeleeLumberJack>();
                    axe.AttackCooldown.StartTimer();
                }
                catch{}
            }
            if(boss)
            {
                try
                {
                    BossAI axe = enemy.gameObject.transform.parent.GetComponent<BossAI>();
                    axe.AttackCooldown.StartTimer();
                }
                catch{}

                try
                {
                    Destroy(axe);
                }
                catch{}
            }
            hitCooldownTimer.StartTimer();
            hit = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Axe")
        {
            axe = other.gameObject.transform.parent.gameObject;    
            if(melee)
            {
                enemy = other.gameObject;
                enemy.SetActive(false);
            }
            getHit();
        }
    }
}