using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soilRetreiverScript : MonoBehaviour
{
    // Start is called before the first frame update
    soilCurrency soilCurrency;
    public int soilAmount = 1;

    bool shouldDestroy;
    void Start()
    {
        soilCurrency = GameObject.Find("Player").GetComponent<soilCurrency>();
    }

    void Update() {
        // float da object
        //transform.Rotate(0.3f, 0.3f, 0.3f);

        if (shouldDestroy && !gameObject.GetComponent<AudioSource>().isPlaying) {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            soilCurrency.addSoils(soilAmount);

            gameObject.transform.localScale = new Vector3(0,0,0);

            gameObject.GetComponent<AudioSource>().Play();

            shouldDestroy = true;

            Debug.Log("SOIL RETRIEVED");
        }
    }
}
