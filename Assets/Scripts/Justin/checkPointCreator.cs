using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointCreator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject checkPointHandling;
    void Start()
    {
        checkPointHandling = transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            checkPointHandling.GetComponent<checkpointHandler>().checkpointPosition = transform.position;
            Destroy(gameObject);
        }
    }

}
