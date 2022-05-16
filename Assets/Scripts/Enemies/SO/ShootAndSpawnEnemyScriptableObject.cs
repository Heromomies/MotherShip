using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object / Shoot And Spawn Enemy")]
public class ShootAndSpawnEnemyScriptableObject : ScriptableObject
{
	public float speed;
	public int damage;
	public int health;

	public bool isShield;
	public float distanceFromMotherShipToStopAndShoot;
	public float fireRateToShoot;
	public float fireRateToSpawnEnemies;

	public bool spawnAnotherEnemyOnDie;
	public List<string> enemiesToSpawnOnShoot;
	public List<string> enemiesToSpawn;
}
