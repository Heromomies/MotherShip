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
            
            _agent.SetDestination(ray.origin);
        }
    }
}
