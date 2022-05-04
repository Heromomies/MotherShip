using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform spawnEnemies;

    #region Singleton

    private static GameManager gameManager;
    public static GameManager Instance => gameManager;

    private void Awake()
    {
        gameManager = this;
    }

    #endregion
}
