using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemiesDamageable 
{
    int CurrentHealth { get; }
	void TakeDamage(int attackDamage);
}
