using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManipulation : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] float maxgrabdistance = 5f;
    [SerializeField] Transform objectholder;

    Rigidbody GrabbedRB;

    void Start()
    {
        
    }

   
    void Update()
    {
        if(GrabbedRB)
        {
            GrabbedRB.MovePosition(objectholder.transform.position);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(GrabbedRB)
            {
                GrabbedRB.isKinematic = false;
                GrabbedRB = null;
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
                   }
                }
            }     
        }
    }
}
