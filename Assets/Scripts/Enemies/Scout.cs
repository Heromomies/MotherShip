using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Enemy, IDamageable
{
    public ParticleSystem explosionSystem;
    
    private float speed;
    private float damage;

    private string nameEnemyToSpawn;
    private bool canSpawnEnemyOnDie;
    private bool canShoot;
    
    private Transform target;
    public float CurrentHealth { get; private set; }
    
    private void Start()
    {
        ParticleSystem eS = Instantiate(explosionSystem);
        explosionSystem = eS;
        damage = statsBase.damage;
        speed = statsBase.speed;
        canSpawnEnemyOnDie = statsBase.spawnAnotherEnemyOnDie;
        canShoot = statsBase.canShoot;
        if (canSpawnEnemyOnDie)
        {
            nameEnemyToSpawn = statsBase.nameEnemyToSpawn;
        }

        target = MotherShipManager.Instance.transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MotherShip"))
        {
            MotherShipManager.Instance.health.TakeDamage(damage);
            TakeDamage(5);
        }
    }
    
    public void TakeDamage(float attackDamage)
    {
        CurrentHealth -= attackDamage;
        
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        explosionSystem.transform.position = transform.position;
        explosionSystem.Play();

        if (canSpawnEnemyOnDie)
        {
            PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawn, GameManager.Instance.spawnEnemies.position, Quaternion.identity, null);
        }

        gameObject.SetActive(false);
    }
    
}
