using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectableCollisionHandling : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Selectable") {
            Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }
    }
}
