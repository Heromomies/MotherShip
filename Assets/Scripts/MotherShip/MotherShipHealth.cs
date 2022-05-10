using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherShipHealth : MonoBehaviour, IDamageable
{
    // Update is called once per frame
    public float CurrentHealth { get; set; }
    public float timeRegen;

    
    
    public Slider sliderHealth;

    private void Start()
    {
        sliderHealth.value = CurrentHealth;
        sliderHealth.maxValue = CurrentHealth;
    }

    private void Update()
    {
        RegenHealth();
    }

    public void ChangeMaxValue(float numberAdded)
    {
        sliderHealth.maxValue += numberAdded;
    }
    
    private void RegenHealth()
    {
        if (timeRegen >= 0 && CurrentHealth <=  sliderHealth.maxValue)
        {
            CurrentHealth += timeRegen;
        }
        else
        {
            timeRegen = 0;
        }
    }
    
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        sliderHealth.value = CurrentHealth;
        
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
       //TODO end of the game
    }
}
