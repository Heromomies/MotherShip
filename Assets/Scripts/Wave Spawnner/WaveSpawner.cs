using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;

    public List<GameObject> buttonsToSpawn;
}

public class WaveSpawner : MonoBehaviour
{
	public Wave[] waves;
	public List<Transform> spawnPoints;

	#region Singleton

	private static WaveSpawner _waveSpawner;
	public static WaveSpawner Instance => _waveSpawner;

	private void Awake()
	{
		_waveSpawner = this;
	}

	#endregion
	
	private Wave currentWave;
	private int currentWaveNumber;
	private float nextSpawnTime;
	
	private GameObject buttonOneToSetActive, buttonTwoToSetActive;
	
	private bool canSpawn = true;
	private bool canSpawnButtons = true;
	private void Update()
	{
		if (waves.Length > 0)
		{
			currentWave = waves[currentWaveNumber];
			SpawnWave();
			GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
			if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
			{
				SpawnButtons();
			}
		}
	}

	private void SpawnButtons()
	{
		if (canSpawnButtons)
		{
			GameManager.Instance.canvasButton.SetActive(true);
			buttonOneToSetActive = currentWave.buttonsToSpawn[Random.Range(0, currentWave.buttonsToSpawn.Count)];
			currentWave.buttonsToSpawn.Remove(buttonOneToSetActive);
		
			buttonTwoToSetActive = currentWave.buttonsToSpawn[Random.Range(0, currentWave.buttonsToSpawn.Count)];

			buttonOneToSetActive.SetActive(true);
			buttonTwoToSetActive.SetActive(true);
			canSpawnButtons = false;
		}
		
		Time.timeScale = 0f;
	}
	
	public void SpawnNextWave()
	{
		Time.timeScale = 1f;
		GameManager.Instance.canvasButton.SetActive(false);
		currentWaveNumber++;
		canSpawn = true;
		canSpawnButtons = true;
		buttonOneToSetActive.SetActive(false);
		buttonTwoToSetActive.SetActive(false);
	}
	
	void SpawnWave()
	{
		if (canSpawn && nextSpawnTime < Time.time)
		{
			GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
			Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
			Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
			currentWave.numberOfEnemies--;
			nextSpawnTime = Time.time + currentWave.spawnInterval;
			if (currentWave.numberOfEnemies <= 0)
			{
				canSpawn = false;
			}
		}
	}
}
