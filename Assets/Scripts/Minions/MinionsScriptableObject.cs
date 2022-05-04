using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object / Player Stats")]
public class MinionsScriptableObject : ScriptableObject
{
	public float speed;
	public int health;
	public int damage;
}
