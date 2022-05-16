using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionsMovement : MonoBehaviour
{
    public MinionsScriptableObject playerStats;
    
    public Transform minionSprite;
    private NavMeshAgent navMeshAgent;

    private float speed;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        speed = playerStats.speed;
    }
    public Texture2D cursorTexture;
  
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 diff = cam.ScreenToWorldPoint(Input.mousePosition) - minionSprite.position;
            diff.Normalize();
 
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            minionSprite.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);

            var target = cam.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            navMeshAgent.destination = target;
        }
    }
}
