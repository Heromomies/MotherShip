using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IDamageable
{
    public ParticleSystem explosionSystem;
    public ShootEnemyScriptableObject shootBase;
    public List<Transform> spawnPointsEnemyOnShoot;
    public bool isSpawner;
    
    private float speed;
    private float damage;
    private float distanceFromMotherShipToStopAndShoot;
    private float fireRate;
    private List<string> nameEnemyToSpawn;
    private bool isShooting;
    
    private Transform target;
    private float currentHealth;
    
    private void Start()
    {
        ParticleSystem eS = Instantiate(explosionSystem);
        explosionSystem = eS;
        damage = shootBase.damage;
        speed = shootBase.speed;
        currentHealth = shootBase.health;
        distanceFromMotherShipToStopAndShoot = shootBase.distanceFromMotherShipToStopAndShoot;
        fireRate = shootBase.fireRateToShoot;

        target = MotherShipManager.Instance.transform;
        
        if (shootBase.enemiesToSpawn.Count > 0)
        {
            nameEnemyToSpawn = shootBase.enemiesToSpawn;
        }

        if (isSpawner)
        {
            isShooting = true; 
            InvokeRepeating(nameof(LaunchBullet), 0, fireRate);
        }
        
    }

    void Update()
    {
        MovementShooter();
    }

    void MovementShooter()
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
    
    void LaunchBullet()
    {
        var randomPoint = Random.Range(0, spawnPointsEnemyOnShoot.Count);
        
        for (int i = 0; i < nameEnemyToSpawn.Count; i++)
        {
            PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawn[i], spawnPointsEnemyOnShoot[randomPoint].position, Quaternion.identity, null);
        }
        
        Debug.Log("Spawn Enemy");
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

        CancelInvoke();

        gameObject.SetActive(false);
    }
    
}
