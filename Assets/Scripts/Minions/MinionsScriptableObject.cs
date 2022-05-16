using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object / Player Stats")]
public class MinionsScriptableObject : ScriptableObject
{
	public float speed;
	public float health;
	public float damage;

	public ParticleSystem particleOnHit;
}
