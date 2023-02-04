using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soilRetreiverScript : MonoBehaviour
{
    // Start is called before the first frame update
    soilCurrency soilCurrency;
    public int soilAmount = 1;
    void Start()
    {
        soilCurrency = GameObject.Find("Player").GetComponent<soilCurrency>();
    }

    void Update() {
        // float da object
        transform.Rotate(0.3f, 0.3f, 0.3f);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            soilCurrency.addSoils(soilAmount);
            Destroy(gameObject);

            Debug.Log("SOIL RETRIEVED");
        }
    }
}
