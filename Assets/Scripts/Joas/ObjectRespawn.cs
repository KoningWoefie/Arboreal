using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectRespawn : MonoBehaviour
{
    public GameObject objectToRespawn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //make the object respawn after falling under y position -10
        if (objectToRespawn.transform.position.y < -10)
        {
            objectToRespawn.transform.position = new Vector3(0, 0, -55);
            Debug.Log("Respawned");
        }
        
    }
}
