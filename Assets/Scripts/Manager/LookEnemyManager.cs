using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookEnemyManager : MonoBehaviour
{
	public float speed;
	public Transform target;
     
	private Vector3 zAxis = new Vector3(0, 0, 1);
 
	void FixedUpdate () 
	{
		Vector3 diff = target.position - transform.position;
		diff.Normalize();
 
		float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
	}

}
