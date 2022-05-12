using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object / Dash Enemy")]
public class DashEnemyScriptableObject : ScriptableObject
{
	[Header("BASE STATS")]
    public float speed;
    public int damage;
    public int health;

    [Header("SHIELD")]
    public bool isShield;
    
    [Header("DASH")]
    public float distanceFromMotherShipToStopAndDash;
    public float timeBeforeDash;
    public float numberDivideToDash;
    
    [Header("DIE")]
    public List<Transform> spawnPoints;
    public bool spawnAnotherEnemyOnDie;
    public List<string> enemiesToSpawn;
}
