using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class rootForesight : MonoBehaviour
{
    //  Root Foresight: The player can use their roots to sense nearby objects and hazards, allowing them to plan their movements and avoid danger. 
    //  When pressing the X button, everything but the "EnemyLayer" layer will become greyscale

    // There are two cameras. The main camera, which sees everything but enemies - and the EnemyCamera, which only sees enemies. These are stacked.

    public Camera mainCamera;
    public Camera EnemyCamera;
    public GameObject PP_Handler;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Set the main camera to greyscale, the PP_Handler has a URP "Volume" component which has a "Color Adjustments" component, just set its "Saturation" to 100
            PP_Handler.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments);
            colorAdjustments.saturation.value = 100;

            Debug.Log("Root Foresight Activated");
        }
        if (Input.GetKeyUp(KeyCode.X))
        {

            Debug.Log("Root Foresight Deactivated");
        }
    }
}
