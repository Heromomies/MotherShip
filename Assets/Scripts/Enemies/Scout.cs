using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Enemy, IDamageable
{
    private float speed;
    private int damage;

    private string nameEnemyToSpawn;
    private bool canSpawnEnemyOnDie;
    
    private Transform _target;

    private void Start()
    {
        damage = statsBase.damage;
        speed = statsBase.speed;
        canSpawnEnemyOnDie = statsBase.spawnAnotherEnemyOnDie;
        if (canSpawnEnemyOnDie)
        {
            nameEnemyToSpawn = statsBase.nameEnemyToSpawn;
        }

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
            TakeDamage(1);
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
        
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (canSpawnEnemyOnDie)
        {
            PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawn, GameManager.Instance.spawnEnemies.position, Quaternion.identity, null);
        }
        
        gameObject.SetActive(false);
    }
    
}
