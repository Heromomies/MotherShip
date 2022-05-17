using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object / Base Enemy")]
public class BaseEnemiesScriptableObject : ScriptableObject
{
   public float speed;
   public float damage;
   public float health;

   public bool isShield;

   public bool spawnAnotherEnemyOnDie;
   public string nameEnemyToSpawn;
}
