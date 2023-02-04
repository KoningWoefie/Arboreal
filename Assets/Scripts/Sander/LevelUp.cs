using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private int healthCost = 150;
    private int staminaCost = 150;
    private int damageCost = 150;

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
}
