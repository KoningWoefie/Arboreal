using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public bool enable = true;
    private float mouseY;
    private float mouseX;
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
        mouseY += Input.GetAxis("Mouse Y");
        mouseX += Input.GetAxis("Mouse X");
        mouseY = Mathf.Clamp(mouseY, -30, 40);
        transform.parent.transform.parent.transform.rotation = Quaternion.Euler(0, mouseX, 0);
        transform.parent.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
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
