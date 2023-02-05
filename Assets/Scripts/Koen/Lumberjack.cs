using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    [SerializeField] private Timer hitCooldown;
    [SerializeField] private Timer attackCooldown;
    private Animator anim;
    //a reference to the player to kill
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject axeSpawnPoint;

    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int rotationSpeed = 10;

    private int currentWaypoint ;
    private int health = 100;

    private bool hit = false;
    private bool standStill = false;
    private bool thrown = false;

    private void Move(){
        //moves the lumberjack towards the player if player is in range but stops at a distance of 10 to throw an axe
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < 30f && Vector3.Distance(transform.position, player.transform.position) > 15f)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            standStill = false;
            anim.SetBool("Walking", true);
        }
        //stand still around the player to shoot an axe
        else if(Vector3.Distance(transform.position, player.transform.position) < 15f && Vector3.Distance(transform.position, player.transform.position) > 5f){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            standStill = true;
            anim.SetBool("Walking", false);
        }
        //if player gets to close turn around and run away from the player
        else if (Vector3.Distance(transform.position, player.transform.position) < 5f){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - player.transform.position), Time.deltaTime * rotationSpeed);
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
            standStill = false;
            anim.SetBool("Walking", true);
        }
        else
        {
            //moves the lumberjack towards the next waypoint
            if (currentWaypoint < waypoints.Length)
            {
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(waypoints[currentWaypoint].position - transform.position), Time.deltaTime * rotationSpeed);
                if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.5f)
                {
                    currentWaypoint++;
                }
            }
            else
            {
                currentWaypoint = 0;
            }
        }
    }
    private void Attack()
    {
        //shoots an axe at the player
        if(standStill == true && attackCooldown.Seconds() == 0)
        {
            anim.SetBool("Throw", true);
            attackCooldown.StartTimer();
        }
        else if(attackCooldown.Seconds() >= 2.7f)
        {
            thrown = false;
            anim.SetBool("Throw", false);
            attackCooldown.StopTimer();
        }
        else if(attackCooldown.Seconds() >= 2f && thrown == false)
        {
            GameObject tempAxe = Instantiate(axe, axeSpawnPoint.transform.position, transform.rotation);
            tempAxe.SetActive(true);
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
            player.GetComponent<soilCurrency>().addSoils(50);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Attack();
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
