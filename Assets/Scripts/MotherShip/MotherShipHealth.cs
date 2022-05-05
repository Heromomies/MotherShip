using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipHealth : MonoBehaviour, IDamageable
{
    // Update is called once per frame
    public int CurrentHealth { get; private set; }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
       
    }
}
