using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeManager : MonoBehaviour
{
   public List<GameObject> objectsToActivate;
   
   private void Update()
   {
        
      if (Input.GetKeyDown(KeyCode.A))
      {
         foreach (var g in objectsToActivate)
         {
            if (!g.activeSelf)
            {
               g.SetActive(true);
            }
            else
            {
               g.SetActive(false);
            }
         }
      }
      
      if (Input.GetKeyDown(KeyCode.E))
      {
         PoolManager.Instance.SpawnObjectFromPool("Worker", transform.position, Quaternion.identity, null);
      }
   }
}
