using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float speed;
    public int damage;
    private Transform _target;

    private void Start()
    {
        _target = MotherShipManager.Instance.transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I'm here");
        if (other.CompareTag("MotherShip"))
        {
            
            MotherShipManager.Instance.health.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
