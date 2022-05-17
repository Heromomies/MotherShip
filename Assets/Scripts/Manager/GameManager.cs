using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform spawnEnemies;
    public GameObject canvasButton;
    
    #region Singleton

    private static GameManager gameManager;
    public static GameManager Instance => gameManager;

    private void Awake()
    {
        gameManager = this;
    }

    #endregion


    public IEnumerator DeactivateParticles(GameObject objectToDeactivate, float timeBeforeDeactivate)
    {
        yield return new WaitForSeconds(timeBeforeDeactivate);

        objectToDeactivate.SetActive(false);
    }
}
