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

            particleOnHit = Instantiate(particleOnHit, transform.position, Quaternion.identity);
            particleOnHit.Play();
        }
    }
}
