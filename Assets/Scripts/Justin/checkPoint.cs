using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private int checkpointNumber;
    void Start()
    {
        checkpointNumber = int.Parse(gameObject.name.Substring(10));
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(checkpointNumber == 1) {
                Debug.Log("Fakka strijder, du er nået til checkpoint 1");
                SelfDestruct();
            }
        }
    }

    void SelfDestruct() {
        Destroy(gameObject);
    }
}
