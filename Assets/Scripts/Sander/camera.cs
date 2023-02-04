using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public bool enable = true;
    private float mouseY;
    // Start is called before the first frame update
    void Start()
    {
        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(enable)
        {
            Rotate();
        }
    }
    void Rotate()
    {
        transform.parent.transform.parent.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        mouseY += Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -40, 40);
        transform.parent.transform.rotation = Quaternion.Euler(-mouseY, 0, 0);
    }
    
    void Enable()
    {
        enable = true;
    }

    void Disable()
    {
        enable = false;
    }
}
