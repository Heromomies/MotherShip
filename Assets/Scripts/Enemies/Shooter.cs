using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IDamageable
{
    public ParticleSystem explosionSystem;
    public ShootEnemyScriptableObject statsBase;
    
    private float speed;
    private float damage;
    private float distanceFromMotherShipToStopAndShoot;
    private float fireRate;
    
    private string nameEnemyToSpawn;
    private bool canSpawnEnemyOnDie;
    private bool canShoot;
    private bool isShooting;
    
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
        canShoot = statsBase.canShoot;
        distanceFromMotherShipToStopAndShoot = statsBase.distanceFromMotherShipToStopAndShoot;
        fireRate = statsBase.fireRate;
       
        if (canSpawnEnemyOnDie || canShoot)
        {
            nameEnemyToSpawn = statsBase.nameEnemyToSpawn;
        }

        target = MotherShipManager.Instance.transform;
    }

    void Update()
    {
        if (!canShoot)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            var dist = Vector2.Distance(transform.position, MotherShipManager.Instance.transform.position);
            if (dist > distanceFromMotherShipToStopAndShoot)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else if(!isShooting && dist <= distanceFromMotherShipToStopAndShoot)
            {
                isShooting = true;
                InvokeRepeating(nameof(LaunchBullet), 0, fireRate);
            }
        }
    }

    void LaunchBullet()
    {
        PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawn, transform.position, Quaternion.identity, null);
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

        if (canShoot)
        {
            CancelInvoke();
        }
        
        gameObject.SetActive(false);
    }
    
}
