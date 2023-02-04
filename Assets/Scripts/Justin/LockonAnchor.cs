using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonAnchor : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject lockonIcon;
    void Start()
    {
        // lockonIcon is the first child of the child of the current obj
        lockonIcon = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the lockon icon to face the camera
        lockonIcon.transform.LookAt(Camera.main.transform);
    }
}
