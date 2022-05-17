using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IDamageable
{
    public ParticleSystem explosionSystem;
    public ShootEnemyScriptableObject shootBase;
    public Transform parentRenderer;
    
    public List<Transform> spawnPointsEnemyOnShoot;
   
    private float speed;
    private float damage;
    private float distanceFromMotherShipToStopAndShoot;
    private float fireRate;
    private List<string> nameEnemyToSpawn;
    private bool isShooting;
    private bool canShootFromTheStart;
    
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
        canShootFromTheStart = shootBase.canShootFromTheStart;
        
        target = MotherShipManager.Instance.transform;

        if (canShootFromTheStart)
        {
            InvokeRepeating(nameof(LaunchBullet), 0, fireRate);
        }
        
        if (shootBase.enemiesToSpawn.Count > 0)
        {
            nameEnemyToSpawn = shootBase.enemiesToSpawn;
        }

        if (spawnPointsEnemyOnShoot.Count == 0)
        {
            spawnPointsEnemyOnShoot = WaveSpawner.Instance.spawnPoints;
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
        
        var dist = Vector2.Distance(tPosition, targetPosition);
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
        for (int i = 0; i < nameEnemyToSpawn.Count; i++)
        {
            var randomPoint = Random.Range(0, spawnPointsEnemyOnShoot.Count);
            PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawn[i], spawnPointsEnemyOnShoot[randomPoint].position, Quaternion.identity, null);
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

        CancelInvoke();

        gameObject.SetActive(false);
    }
    
}
