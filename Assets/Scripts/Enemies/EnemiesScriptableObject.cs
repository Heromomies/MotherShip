using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object / Enemy")]
public class EnemiesScriptableObject : ScriptableObject
{
   public float speed;
   public int damage;
   public int health;
}
