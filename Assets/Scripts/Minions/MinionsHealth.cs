using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsHealth : MonoBehaviour, IDamageable
{
	public MinionsScriptableObject playerStats;
	private float health;

	private void Awake()
	{
		health = playerStats.health;
	}

	public void TakeDamage(float attackDamage)
	{
		health -= attackDamage;
		
		if (health <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		MotherShipManager.Instance.RemoveMinionFromList(gameObject);
		gameObject.SetActive(false);
	}
}