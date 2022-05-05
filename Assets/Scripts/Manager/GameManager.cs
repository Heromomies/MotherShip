using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform spawnEnemies;

    [Header("EXPERIENCE")]
    public Slider sliderExperience;
    public int currentExperience;
    public int targetExperience;
    public int experienceToAddByLevel;
    
    public int levelNumber;
    
    #region Singleton

    private static GameManager gameManager;
    public static GameManager Instance => gameManager;

    private void Awake()
    {
        gameManager = this;
    }

    #endregion

    public void AddExperience(int xp)
    {
        currentExperience += xp;

        while (currentExperience >= targetExperience)
        {
            currentExperience = currentExperience - targetExperience;
            levelNumber++;

            targetExperience += experienceToAddByLevel;
        }

        sliderExperience.value = currentExperience;
        sliderExperience.maxValue = targetExperience;
    }
    
    
}
