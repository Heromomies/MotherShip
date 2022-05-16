using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAndSpawn : MonoBehaviour, IDamageable
{
    public ParticleSystem explosionSystem;
    public ShootAndSpawnEnemyScriptableObject shootBase;
    public Transform spawnPointsEnemyOnShoot;
    public List<Transform> spawnPointsEnemyOnDie;
    public List<Transform> spawnPointsEnemyOnRate;

    public Transform parentRenderer;
    
    private float speed;
    private float damage;
    private float distanceFromMotherShipToStopAndShoot;
    private float fireRate;
    private float fireRateToSpawnEnemies;
    
    private List<string> nameEnemyToSpawn;
    private List<string> nameEnemyToSpawnOnShoot;
    private bool canSpawnEnemyOnDie;
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
        canSpawnEnemyOnDie = shootBase.spawnAnotherEnemyOnDie;
        distanceFromMotherShipToStopAndShoot = shootBase.distanceFromMotherShipToStopAndShoot;
        fireRate = shootBase.fireRateToShoot;
        fireRateToSpawnEnemies = shootBase.fireRateToSpawnEnemies;
        
        target = MotherShipManager.Instance.transform;
        
        if (shootBase.enemiesToSpawn.Count > 0)
        {
            nameEnemyToSpawn = shootBase.enemiesToSpawn;
        } 
        if (shootBase.enemiesToSpawnOnShoot.Count > 0)
        {
            nameEnemyToSpawnOnShoot = shootBase.enemiesToSpawnOnShoot;
        }

        if (spawnPointsEnemyOnRate.Count > 0)
        {
            InvokeRepeating(nameof(SpawnEnemy), 0, fireRateToSpawnEnemies);
        }
    }

    void Update()
    {
        MovementShooter();
    }

    void MovementShooter()
    {
        var tPosition = transform.position;
        var targetPosition = target.position;
        Vector3 diff = targetPosition - tPosition;
        diff.Normalize();
 
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        parentRenderer.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        
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

    void SpawnEnemy()
    {
        for (int i = 0; i < nameEnemyToSpawn.Count; i++)
        {
            PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawn[i], spawnPointsEnemyOnRate[i].position, Quaternion.identity, null);
        }
    }
    
    void LaunchBullet()
    {
        for (int i = 0; i < nameEnemyToSpawnOnShoot.Count; i++)
        {
            PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawnOnShoot[i], spawnPointsEnemyOnShoot.position, Quaternion.identity, null);
        }
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
            for (int i = 0; i < nameEnemyToSpawn.Count; i++)
            {
                PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawn[i], spawnPointsEnemyOnDie[i].position, Quaternion.identity, null);
            } 
        }

        CancelInvoke();

        gameObject.SetActive(false);
    }

}
