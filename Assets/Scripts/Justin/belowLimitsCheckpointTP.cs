using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class belowLimitsCheckpointTP : MonoBehaviour
{
    // Start is called before the first frame update
    public int belowLimits;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").transform.position.y < belowLimits)
        {
            GetComponent<checkpointHandler>().GoToCheckpoint();
        }
    }
}
