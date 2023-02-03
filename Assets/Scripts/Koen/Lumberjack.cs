using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    [SerializeField] private Timer t;
    //a reference to the player to kill
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int rotationSpeed = 10;

    private int currentWaypoint ;
    private int health = 100;

    private bool hit = false;

    private void Move(){
        //moves the lumberjack towards the player if player is in range but stops at a distance of 10 to throw an axe
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < 30f && Vector3.Distance(transform.position, player.transform.position) > 15f)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
             transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
        }
        //stand still around the player to shoot an axe
        else if(Vector3.Distance(transform.position, player.transform.position) < 15f && Vector3.Distance(transform.position, player.transform.position) > 8f){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
        }
        //if player gets to close turn around and run away from the player
        else if (Vector3.Distance(transform.position, player.transform.position) < 8f){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - player.transform.position), Time.deltaTime * rotationSpeed);
            transform.position += transform.forward * Time.deltaTime * moveSpeed;

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

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Lumberjack took " + damage + " damage");
    }

    private void Update()
    {
        Move();
        if(t.Seconds() >= 0.3f)
        {
            hit = false;
            t.StopTimer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Root" && hit == false)
        {
            TakeDamage(10);
            hit = true;
            t.StartTimer();
        }
    }

}
