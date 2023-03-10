using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private GameObject axeModel;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(GameObject.Find("Player").transform);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 180, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);;
    }

    // Update is called once per frame
    void Update()
    {
        axeModel.transform.Rotate(200 * Time.deltaTime, 0, 0);
        transform.position -= transform.forward * 20 * Time.deltaTime;
    }
}
