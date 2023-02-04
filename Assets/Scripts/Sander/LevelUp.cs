using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    private int healthCost = 150;
    private int staminaCost = 150;
    private int damageCost = 150;

    private int currentSoils;

    private GameObject shop;

    public bool isShopOpen = false;

    void Start() {
        shop = GameObject.Find("shopUpgrade");
        shop.transform.localScale = new Vector3(0, 0, 0);

    }

    public void showShop()
    {
        shop.transform.localScale = new Vector3(1, 1, 1);
        Cursor.lockState = CursorLockMode.None;

        DisableEverything();
    }

    public void hideShop()
    {
        shop.transform.localScale = new Vector3(0, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;

        EnableEverything();
    }

    void Update()
    {
        currentSoils = GameObject.Find("Player").GetComponent<soilCurrency>().soilsAmount;

        GameObject.Find("healthCostTxt").GetComponent<Text>().text = healthCost + " Soils";
        if(healthCost <= currentSoils) {
            GameObject.Find("healthBuyBtn").GetComponent<Button>().interactable = true;
        } else {
            GameObject.Find("healthBuyBtn").GetComponent<Button>().interactable = false;
        }

        GameObject.Find("staminaCostTxt").GetComponent<Text>().text = staminaCost + " Soils";
        if(staminaCost <= currentSoils) {
            GameObject.Find("staminaBuyBtn").GetComponent<Button>().interactable = true;
        } else {
            GameObject.Find("staminaBuyBtn").GetComponent<Button>().interactable = false;
        }

        GameObject.Find("damageCostTxt").GetComponent<Text>().text = damageCost + " Soils";
        if(damageCost <= currentSoils) {
            GameObject.Find("damageBuyBtn").GetComponent<Button>().interactable = true;
        } else {
            GameObject.Find("damageBuyBtn").GetComponent<Button>().interactable = false;
        }

        if(Input.GetKeyDown(KeyCode.E) && isShopOpen) {
            showShop();
        }
        if(Input.GetKeyDown(KeyCode.X) || (!isShopOpen && GameObject.Find("shopUpgrade").transform.localScale == new Vector3(1, 1, 1))) {
            hideShop();
        }
    }

    public void levelUpHealth()
    {
        EnhancedDoosLocomotion player = GameObject.Find("Player").GetComponent<EnhancedDoosLocomotion>();
        soilCurrency soil = GameObject.Find("Player").GetComponent<soilCurrency>();
        player.MaxHealth += 10;
        soil.removeSoils(healthCost);
        float healthTemp = healthCost;
        healthTemp *= 1.2f;
        healthCost = (int)healthTemp;
    }

    public void levelUpStamina()
    {
        EnhancedDoosLocomotion player = GameObject.Find("Player").GetComponent<EnhancedDoosLocomotion>();
        soilCurrency soil = GameObject.Find("Player").GetComponent<soilCurrency>();
        player.MaxStamina += 10;
        soil.removeSoils(staminaCost);
        float staminaTemp = staminaCost;
        staminaTemp *= 1.2f;
        staminaCost = (int)staminaTemp;
    }

    public void levelUpDamage()
    {
        Melee player = GameObject.Find("Player").GetComponentInChildren<Melee>();
        soilCurrency soil = GameObject.Find("Player").GetComponent<soilCurrency>();
        player.Damage += 10;
        soil.removeSoils(damageCost);
        float damageTemp = damageCost;
        damageTemp *= 1.2f;
        damageCost = (int)damageTemp;
    }

    public void DisableEverything() {
        GameObject Player = GameObject.Find("Player");
        Debug.Log("DISABLED EVERYTHING");
        // set level speed to 0
        Camera.main.GetComponent<camera>().enabled = false;
        Player.GetComponent<EnhancedDoosLocomotion>().enabled = false;
    }

    public void EnableEverything() {
        GameObject Player = GameObject.Find("Player");
        Debug.Log("ENABLED EVERYTHING");
        // set level speed to 1
        Camera.main.GetComponent<camera>().enabled = true;
        Player.GetComponent<EnhancedDoosLocomotion>().enabled = true;
    }
}
