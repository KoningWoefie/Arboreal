using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    //a reference to the player to kill
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] waypoints;

    private int currentWaypoint ;

    private void Move(){
        //moves the lumberjack towards the player if player is in range
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < 10f)
        {
            transform.position += transform.forward * Time.deltaTime * 5f;
             transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * 10);
        }
        else
        {
            //moves the lumberjack towards the next waypoint
            if (currentWaypoint < waypoints.Length)
            {
                transform.position += transform.forward * Time.deltaTime * 5f;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(waypoints[currentWaypoint].position - transform.position), Time.deltaTime * 10);
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

    private void Attack(){
        //if the lumberjack is close enough to the player, destroy the player
        if (Vector3.Distance(transform.position, player.transform.position) < 0.5f)
        {
            Destroy(player);
        }
    }

    private void Update()
    {
        Move();
        Attack();


    }
}
