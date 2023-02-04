using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointHandler : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public Vector3 checkpointPosition;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    
    public void GoToCheckpoint() {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = checkpointPosition;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
