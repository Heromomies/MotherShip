using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
	[Header("EXPERIENCE")] public WaveSpawner waveSpawner;
	public MinionsScriptableObject minionScriptableObject;
	public MotherShipHealth motherShipHealth;
	
	#region Singleton

	private static ButtonManager _buttonManager;
	public static ButtonManager Instance => _buttonManager;

	private void Awake()
	{
		_buttonManager = this;
	}

	#endregion

	

	public void ChangeMinionMax(int numberMinions)
	{
		MotherShipManager.Instance.numberMinionsMax += numberMinions;
	}
	public void ChangeHealthMinions(float healthNumber)
	{
		minionScriptableObject.health += healthNumber;
	}
	public void ChangeHealthMotherShip(float healthNumber)
	{
		motherShipHealth.ChangeMaxValue(healthNumber);
	}
	public void ChangeMotherShipSpawnRate(float spawnRate)
	{
		MotherShipManager.Instance.timeBeforeSpawnMinion -= spawnRate;
	}
	public void ChangeMinionsDamage(float minionsDamage)
	{
		minionScriptableObject.damage += minionsDamage;
	}
	public void ChangeHealthRegenMotherShip(float regenNumber)
	{
		motherShipHealth.timeRegen = regenNumber;
	}
	public void ChangeMinionSpeed(float speedNumber)
	{
		minionScriptableObject.speed += speedNumber;
	}
	public void ChangeSpecial()
	{
		
	}

	public void ButtonClicked()
	{
		waveSpawner.SpawnNextWave();
	}
	
}