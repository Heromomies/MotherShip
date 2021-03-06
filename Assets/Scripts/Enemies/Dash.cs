using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Dash : MonoBehaviour, IDamageable
{
    public ParticleSystem explosionSystem;
    public DashEnemyScriptableObject statsBase;

    [Header("FEEDBACK SETTINGS")] public Color colorToChange;
    public Color baseColor;
    public float durationFadeIn, durationFadeOut, durationShake, strengthShake;
    
    
    private float speed;
    private float damage;
    private float distanceFromMotherShipToStopAndShoot;
    private float numberDivideToDash;
    private float timeBeforeDash;
   
    private List<string> nameEnemyToSpawn;
    private List<Transform> spawnPointsEnemy = new List<Transform>();
    private bool canSpawnEnemyOnDie;
    private bool isDash;
    
    private Transform target;
    private float currentHealth;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem eS = Instantiate(explosionSystem);
        explosionSystem = eS;
        damage = statsBase.damage;
        speed = statsBase.speed;
        currentHealth = statsBase.health;
        canSpawnEnemyOnDie = statsBase.spawnAnotherEnemyOnDie;
        distanceFromMotherShipToStopAndShoot = statsBase.distanceFromMotherShipToStopAndDash;
        spawnPointsEnemy = statsBase.spawnPoints;
        numberDivideToDash = statsBase.numberDivideToDash;
        timeBeforeDash = statsBase.timeBeforeDash;

        spriteRenderer = GetComponent<SpriteRenderer>(); //TODO GetComponentInChildren in the future
        
        target = MotherShipManager.Instance.transform;
        
        if (statsBase.enemiesToSpawn.Count > 0)
        {
            nameEnemyToSpawn = statsBase.enemiesToSpawn;
        }
    }

    void Update()
    {
        MovementShooter();
    }

    void MovementShooter()
    {
        var dist = Vector2.Distance(transform.position, MotherShipManager.Instance.transform.position);
        if (dist > distanceFromMotherShipToStopAndShoot || isDash)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if(!isDash && dist <= distanceFromMotherShipToStopAndShoot)
        {
            StartCoroutine(TimeBeforeDashing());
        }
    }

    private IEnumerator TimeBeforeDashing()
    {
        spriteRenderer.DOColor(colorToChange, durationFadeIn);
        transform.DOShakePosition(durationShake, strengthShake, 5, 0);
        
        yield return new WaitForSeconds(timeBeforeDash);
        isDash = true;
        transform.DOMove(target.position / numberDivideToDash, 0.2f);
        
        spriteRenderer.DOColor(baseColor, durationFadeOut);
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
                PoolManager.Instance.SpawnObjectFromPool(nameEnemyToSpawn[i], spawnPointsEnemy[i].position, Quaternion.identity, null);
            } 
        }

        CancelInvoke();

        gameObject.SetActive(false);
    }
}
