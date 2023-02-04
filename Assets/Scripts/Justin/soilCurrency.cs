using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soilCurrency : MonoBehaviour
{
    private int soilsAmount = 0;
    [SerializeField]private GameObject soilRetreiver;
    // Start is called before the first frame update

    public void addSoils(int amount) {
        soilsAmount += amount;
    }

    public void removeSoils(int amount) {
        soilsAmount -= amount;
    }

    public void resetSoils()
    {
        Instantiate(soilRetreiver, transform.position, Quaternion.Euler(0,0,0));
        soilsAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (soilsAmount != 0) {
            GameObject.Find("SoilsTxt").GetComponent<Text>().text = soilsAmount.ToString();
        } else {
            GameObject.Find("SoilsTxt").GetComponent<Text>().text = "broke bitch";
        }
    }
}
