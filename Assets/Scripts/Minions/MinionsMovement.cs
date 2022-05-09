using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionsMovement : MonoBehaviour
{
    public MinionsScriptableObject playerStats;
    
    public float angle;
    public float rayRange;
    public Transform minionSprite;
    public float angleRaycast;
    
    public int numberOfRays ;
    public  float totalAngle;

    public LayerMask layerPlayer;

    private NavMeshAgent _navMeshAgent;
    private float delta; 
    private Vector3 pos;

    private float speed;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
        speed = playerStats.speed;
    }
    public Texture2D cursorTexture;
  
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
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
            
           // var ray = _cam.ScreenPointToRay(Input.mousePosition);

            //_navMeshAgent.SetDestination(ray.origin);
            
            var t = transform;
            
            delta = totalAngle / numberOfRays;
            pos = transform.position;
            
            for (int i = 0; i < numberOfRays; i++)
            {
                var dir = Quaternion.Euler(0, 0, i * delta) * transform.right;               
                Debug.DrawRay(pos, dir * rayRange, Color.green);
                
                RaycastHit2D hit = Physics2D.Raycast(pos, dir* rayRange, rayRange, layerPlayer);
                
                if (hit.collider != null && hit.collider.name != gameObject.name)
                {
                }
                else
                {
                    var target = _cam.ScreenToWorldPoint(Input.mousePosition);
                    target.z = 0;
                    _navMeshAgent.destination = target;
                    
                    
                    speed = playerStats.speed;
                   /* Vector3 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);*/
                }
            }
            
        }
    }
   /* 
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        delta = totalAngle / numberOfRays;
        pos = transform.position;
            
        for (int i = 0; i < numberOfRays; i++)
        {
            var dir = Quaternion.Euler(0, 0, i * delta) * transform.right;            
            Debug.DrawRay(pos, dir * rayRange, Color.green);

            RaycastHit2D hit = Physics2D.Raycast(pos, dir* rayRange, rayRange, layerPlayer);
            
            if (hit.collider != null && hit.collider.name != gameObject.name)
            {
                Debug.DrawRay(pos, dir * rayRange, Color.red);
            }
        }   
    }
#endif
   */
}
