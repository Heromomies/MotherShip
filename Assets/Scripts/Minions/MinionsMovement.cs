using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionsMovement : MonoBehaviour
{
    public MinionsScriptableObject playerStats;
    
    private NavMeshAgent _agent;
    private float speed;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
        speed = playerStats.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            
            Vector3 diff = _cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
 
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
            
            _agent.SetDestination(ray.origin);
        }
    }
}
