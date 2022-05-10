using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
	[Header("EXPERIENCE")] public WaveSpawner waveSpawner;
	public MinionsScriptableObject minionScriptableObject;
	
	#region Singleton

	private static ButtonManager _buttonManager;
	public static ButtonManager Instance => _buttonManager;

	private void Awake()
	{
		_buttonManager = this;
	}

	#endregion

	

	public void AddMinionMax()
	{
		MotherShipManager.Instance.numberMinionsMax++;
	}
	public void AddHealthMinions()
	{
		minionScriptableObject.health++;
	}
	public void IncreaseMotherShipSpawnRate()
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

	public void ButtonClicked()
	{
		waveSpawner.SpawnNextWave();
	}
	
}