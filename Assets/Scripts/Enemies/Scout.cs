using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : MonoBehaviour, IDamageable
{
    public ParticleSystem explosionSystem;
    public BaseEnemiesScriptableObject statsBase;
    
    private float speed;
    private float damage;

    private string nameEnemyToSpawn;
    private bool canSpawnEnemyOnDie;

    private Transform target;
    private float currentHealth;
    
    private void Start()
    {
        ParticleSystem eS = Instantiate(explosionSystem);
        explosionSystem = eS;
        damage = statsBase.damage;
        speed = statsBase.speed;
        currentHealth = statsBase.health;
        canSpawnEnemyOnDie = statsBase.spawnAnotherEnemyOnDie;

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
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MinionsHealth>().TakeDamage(damage);
        }
    }
    
    public void TakeDamage(float attackDamage)
    {
        currentHealth -= attackDamage;
        
        if (currentHealth <= 0)
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
