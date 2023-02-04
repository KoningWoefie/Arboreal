using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManipulation : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] float maxgrabdistance = 34.762f;
    [SerializeField] Transform objectholder;
    [SerializeField] Material highlightmaterial;

    Rigidbody GrabbedRB;
    public bool isGrabbed;

    void Start()
    {
        
    }

   
    void Update()
    {
        if(GrabbedRB)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f){
                if (Input.GetKey(KeyCode.Z)){
                GrabbedRB.transform.Rotate(0, Input.GetAxis("Mouse ScrollWheel") * 20, 0);
                return;  
            }
                objectholder.transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * 2);
            }
            else
            {
                GrabbedRB.MovePosition(objectholder.transform.position);
            }
            
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(GrabbedRB)
            {
                GrabbedRB.gameObject.layer = 0;
                GrabbedRB.isKinematic = false;
                GrabbedRB = null;
                isGrabbed = false;
            }
            else
            {
                RaycastHit hit;
                Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if(Physics.Raycast(ray, out hit, maxgrabdistance))
                {
                   GrabbedRB = hit.transform.GetComponent<Rigidbody>();
                   if(GrabbedRB){
                        GrabbedRB.isKinematic = true;
                        isGrabbed = true;
                        GrabbedRB.gameObject.layer = 7;
                   }
                }
            }     
        }
    }
}
