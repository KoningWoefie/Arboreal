using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManipulation : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] float maxgrabdistance = 34.762f;
    [SerializeField] Transform objectholder;
    [SerializeField] Material highlightmaterial;
    [SerializeField] Material defaultmaterial;

    Rigidbody GrabbedRB;
    public bool isGrabbed;

    void Start()
    {
        
    }

   
    void Update()
    {
        if(GrabbedRB)
        {
            GrabbedRB.GetComponent<Renderer>().material = highlightmaterial;
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
            if(GrabbedRB && GrabbedRB.gameObject.layer == 7)
            {
                GrabbedRB.isKinematic = false;
                GrabbedRB.gameObject.layer = 10;
                GrabbedRB = null;
                isGrabbed = false;
            }
            else
            {
                RaycastHit hit;
                Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if(Physics.Raycast(ray, out hit, maxgrabdistance) && (hit.transform.gameObject.layer == 7 || hit.transform.gameObject.layer == 10))
                {
                   GrabbedRB = hit.transform.GetComponent<Rigidbody>();
                   if(GrabbedRB && (GrabbedRB.gameObject.layer == 7 || GrabbedRB.gameObject.layer == 10)){
                        GrabbedRB.isKinematic = true;
                        isGrabbed = true;
                        GrabbedRB.gameObject.layer = 7;
                   }
                }
            }     
        }
    }
}
