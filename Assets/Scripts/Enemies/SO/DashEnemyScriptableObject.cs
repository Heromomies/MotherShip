using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object / Dash Enemy")]
public class DashEnemyScriptableObject : ScriptableObject
{
    public float speed;
    public int damage;
    public int health;

    public bool isShield;
    public float distanceFromMotherShipToStopAndShoot;
    public List<Transform> spawnPoints;
	
    public bool spawnAnotherEnemyOnDie;
    public List<string> enemiesToSpawn;
}
