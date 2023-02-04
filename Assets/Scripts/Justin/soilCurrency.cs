using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soilCurrency : MonoBehaviour
{
    private int soilsAmount = 0;
    // Start is called before the first frame update

    public void addSoils(int amount) {
        soilsAmount += amount;
    }

    public void removeSoils(int amount) {
        soilsAmount -= amount;
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
