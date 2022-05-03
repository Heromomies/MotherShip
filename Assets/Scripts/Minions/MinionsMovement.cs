using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionsMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            
            _agent.SetDestination(ray.origin);
        }
    }
}
