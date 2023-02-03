using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private bool enable;
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
