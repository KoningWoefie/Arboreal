using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLumberJack : MonoBehaviour
{
    [SerializeField] private Timer hitCooldown;
    [SerializeField] private Timer attackCooldown;
    public Timer AttackCooldown
    {
        get { return attackCooldown; }
    }
    //a reference to the player to kill
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject axe;
    public GameObject Axe
    {
        get { return axe; }
    }
    [SerializeField] private Transform[] waypoints;

    private Animator anim;

    [SerializeField] private int moveSpeed = 1;
    [SerializeField] private int rotationSpeed = 10;

    private int currentWaypoint ;
    [SerializeField]private int health = 100;

    private bool hit = false;
    private bool standStill = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        axe.SetActive(false);
    }

    private void Move(){
        //moves the lumberjack towards the player if player is in range but stops at a distance of 10 to throw an axe
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < 30f && Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed / 2;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            standStill = false;
            anim.SetBool("Walking", true);
        }
        //stand still around the player to shoot an axe
        else if(Vector3.Distance(transform.position, player.transform.position) < 3f){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            standStill = true;
            anim.SetBool("Walking", false);
            
            Attack();
        }
        else
        {
            //moves the lumberjack towards the next waypoint
            if (currentWaypoint < waypoints.Length)
            {
                anim.SetBool("Walking", true);
                transform.position += transform.forward * Time.deltaTime * moveSpeed / 2;
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
        if(attackCooldown.Seconds() == 0)
        {
            anim.SetBool("Melee", true);
            axe.SetActive(true);
            attackCooldown.StartTimer();
            Debug.Log("AXE SET TO ACTIVE!!!");
        }
        if(standStill && attackCooldown.Seconds() >= 1.4f)
        {
            anim.SetBool("Melee", false);
            attackCooldown.StopTimer();
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
