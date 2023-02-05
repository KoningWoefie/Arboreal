using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private Timer hitCooldown;
    [SerializeField] private Timer attackCooldown;
    [SerializeField] private audioHandlerScript audioHandler;    // The audio handler.
    public Timer AttackCooldown
    {
        get { return attackCooldown; }
    }
    //a reference to the player to kill
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject axeSpawnPoint;
    [SerializeField] private GameObject rangedAxe;
    [SerializeField] private GameObject meleeAxe;

    public GameObject RangedAxe
    {
        get { return rangedAxe; }
    }

    private Animator anim;

    [SerializeField] private int moveSpeed = 1;
    [SerializeField] private int rotationSpeed = 10;

    private int currentWaypoint ;
    [SerializeField]private int health = 100;

    private bool hit = false;
    private bool standStill = false;
    private bool thrown = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Move(){
        //moves the lumberjack towards the player if player is in range but stops at a distance of 10 to throw an axe
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < 30f && Vector3.Distance(transform.position, player.transform.position) > 15f)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed / 2;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            standStill = false;
            anim.SetBool("Walking", true);
        }
        else if(Vector3.Distance(transform.position, player.transform.position) < 15f && Vector3.Distance(transform.position, player.transform.position) > 9f)
        {
            if(!standStill)
            {
                transform.position += transform.forward * Time.deltaTime * moveSpeed / 2;
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            RangedAttack();
        }
        //stand still around the player to shoot an axe
        else if(Vector3.Distance(transform.position, player.transform.position) < 9f){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            standStill = true;
            anim.SetBool("Walking", false);
            MeleeAttack();
        }
    }
    private void MeleeAttack()
    {
        if(attackCooldown.Seconds() == 0)
        {
            anim.SetBool("Melee", true);
            meleeAxe.SetActive(true);
            attackCooldown.StartTimer();
            audioHandler.PlayRandomAudioSourceFromList(1, false);
            Debug.Log("Melee attack");
        }
        if(standStill && attackCooldown.Seconds() >= 1.4f)
        {
            anim.SetBool("Melee", false);
            attackCooldown.StopTimer();
        }
    }

    private void RangedAttack()
    {
        if(attackCooldown.Seconds() == 0)
        {
            anim.SetBool("Throw", true);
            standStill = true;
            attackCooldown.StartTimer();
            audioHandler.PlayRandomAudioSourceFromList(1, false);
            Debug.Log("Ranged attack");
        }
        else if(attackCooldown.Seconds() >= 2.7f)
        {
            thrown = false;
            anim.SetBool("Throw", false);
            attackCooldown.StopTimer();
        }
        else if(attackCooldown.Seconds() >= 2f && thrown == false)
        {
            GameObject tempAxe = Instantiate(rangedAxe, axeSpawnPoint.transform.position, transform.rotation);
            tempAxe.SetActive(true);
            standStill = false;
            thrown = true;
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Lumberjack took " + damage + " damage");
    }

    void Die()
    {
        if(health <= 0)
        {
            player.GetComponent<soilCurrency>().addSoils(5000);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Move();
        Die();
        if(hitCooldown.Seconds() >= 0.3f)
        {
            hit = false;
            hitCooldown.StopTimer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Root" && hit == false)
        {
            TakeDamage(10);
            hit = true;
            hitCooldown.StartTimer();
        }
    }
}
