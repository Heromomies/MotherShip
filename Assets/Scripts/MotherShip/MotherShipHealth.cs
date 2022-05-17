using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherShipHealth : MonoBehaviour, IDamageable
{
    // Update is called once per frame
    public float currentHealth;
    public float timeRegen;
    public GameObject panelEnd;

    public Slider sliderHealth;

    private void Start()
    {
        sliderHealth.maxValue = currentHealth;
        sliderHealth.value = currentHealth;
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
        if (timeRegen > 0 && currentHealth < sliderHealth.maxValue)
        {
            currentHealth += timeRegen * Time.deltaTime;
            sliderHealth.value = currentHealth;
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        sliderHealth.value = currentHealth;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        panelEnd.SetActive(true);
    }
}
