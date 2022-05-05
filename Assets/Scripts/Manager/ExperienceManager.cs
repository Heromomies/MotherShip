using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
	[Header("EXPERIENCE")] public Slider sliderExperience;
	public int currentExperience;
	public int targetExperience;
	public int experienceToAddByLevel;

	public int levelNumber;

	public MinionsScriptableObject minionScriptableObject;
	
	#region Singleton

	private static ExperienceManager experienceManager;
	public static ExperienceManager Instance => experienceManager;

	private void Awake()
	{
		experienceManager = this;
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

	public void AddMinionMax()
	{
		MotherShipManager.Instance.numberMinionsMax++;
	}
	public void AddHealthMinions()
	{
		minionScriptableObject.health++;
	}
	public void AddMinionsSpawnRate()
	{
		MotherShipManager.Instance.timeBeforeSpawnMinion--;
	}
	public void AddMinionsDamage()
	{
		minionScriptableObject.damage++;
	}
	public void AddHealthRegenMotherShip()
	{
		
	}
	public void AddMinionSpeed()
	{
		minionScriptableObject.speed++;
	}
	public void AddSpecial()
	{
		
	}
	
}