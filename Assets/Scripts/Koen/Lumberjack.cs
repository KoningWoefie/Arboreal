using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    //a reference to the player that we can use
    public GameObject player;

    private void Update()
    {
        //moves the lumberjack towards the player if player is in range
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < 10f)
        {
            transform.position += transform.forward * Time.deltaTime * 5f;
             transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * 10);
        }

        //if the lumberjack is close enough to the player, destroy the player
        if (Vector3.Distance(transform.position, player.transform.position) < 0.5f)
        {
            Destroy(player);
        }

    }
}
