using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scout : MonoBehaviour, IDamageable
{
    public ParticleSystem explosionSystem;
    public BaseEnemiesScriptableObject statsBase;
    public Transform parentRenderer;
    
    private float speed;
    private float damage;

    private string nameEnemyToSpawn;
    private bool canSpawnEnemyOnDie;

    private Transform target;
    private float currentHealth;
    
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    
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
        var tPosition = transform.position;
        var targetPosition = target.position;
        Vector3 diff = targetPosition - tPosition;
        diff.Normalize();
 
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        parentRenderer.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        
        transform.position = Vector2.MoveTowards(tPosition, targetPosition, speed * Time.deltaTime);
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
        _camera.DOShakePosition(0.2f, 0.3f, 90, 100);
        
        explosionSystem.transform.position = transform.position;    
        explosionSystem.Play();

        if (canSpawnEnemyOnDie)
        {
            PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawn, GameManager.Instance.spawnEnemies.position, Quaternion.identity, null);
        }
        
        gameObject.SetActive(false);
    }
}
