using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutEnemy : Enemy, IEnemiesDamageable
{
    private float speed;
    private int damage;
    private Transform _target;

    private void Start()
    {
        damage = statsBase.damage;
        speed = statsBase.speed;

        _target = MotherShipManager.Instance.transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MotherShip"))
        {
            MotherShipManager.Instance.health.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    public int CurrentHealth
    {
        get;
        private set;
    }
    
    public void TakeDamage(int attackDamage)
    {
        CurrentHealth -= attackDamage;

        if (attackDamage <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Die method
    }
}
