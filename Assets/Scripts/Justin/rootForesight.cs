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
    public Volume PP_Handler;


    void Start() {
        // Set the main camera to greyscale, the PP_Handler has a URP "Volume" component which has a "Color Adjustments" component, just set its "Saturation" to 100
        PP_Handler.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments);
        colorAdjustments.saturation.value = 0;

        EnemyCamera.enabled = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RootSight(true);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            RootSight(false);
        }

        // Remove the EnemyLayer of all GameObjects more than 10 units away from the player, re-add it if they are closer than 10 units
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            if (Vector3.Distance(enemy.transform.position, transform.position) > 50) {
                enemy.layer = 0;
            } else {
                enemy.layer = 6;
            }
        }
    }

    public void RootSight(bool Enabled) {
        if (Enabled) {
            PP_Handler.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments);
            colorAdjustments.saturation.value = -100;

            EnemyCamera.enabled = true;
        } else {
            PP_Handler.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments);
            colorAdjustments.saturation.value = 0;

            EnemyCamera.enabled = false;
        }
    }
}
