using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionsMovement : MonoBehaviour
{
    public MinionsScriptableObject playerStats;
    
    public Transform minionSprite;
    
    private NavMeshAgent _navMeshAgent;

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
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 diff = _cam.ScreenToWorldPoint(Input.mousePosition) - minionSprite.position;
            diff.Normalize();
 
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            minionSprite.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
            
            var target = _cam.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            _navMeshAgent.destination = target;
        }
    }
}
