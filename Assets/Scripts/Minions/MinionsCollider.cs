using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsCollider : MonoBehaviour
{
    public MinionsScriptableObject playerStats;
    public ParticleSystem particleOnHit;
    private float damage;

    private void Awake()
    {
        damage = playerStats.damage;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);

            GameObject a = PoolManager.Instance.SpawnObjectFromPool("ParticleHit", transform.position, Quaternion.identity, null);
            particleOnHit = a.GetComponent<ParticleSystem>();

            particleOnHit.Play();
        }
    }
}
