using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object / Shoot Enemy")]
public class ShootEnemyScriptableObject : ScriptableObject
{
	public float speed;
	public int damage;
	public int health;

	public bool isShield;
	public bool canShoot;
	public float distanceFromMotherShipToStopAndShoot;
	public float fireRate;

	public bool spawnAnotherEnemyOnDie;
	public string nameEnemyToSpawn;
}
