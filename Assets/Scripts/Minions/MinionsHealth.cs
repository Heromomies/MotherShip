using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsHealth : MonoBehaviour, IDamageable
{
	public MinionsScriptableObject playerStats;
	private int health;

	private void Awake()
	{
		health = playerStats.health;
	}

	public void TakeDamage(int attackDamage)
	{
		health -= attackDamage;
		if (health <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Debug.Log("A minion died.");
		gameObject.SetActive(false);
	}
}