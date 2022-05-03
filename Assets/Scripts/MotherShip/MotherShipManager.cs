using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherShipManager : MonoBehaviour
{
    public Slider sliderSpawner;
    public float timeBeforeSpawnMinion;
    public GameObject prefabMinion;
    public Transform transformSpawnMinion;

    public MotherShipHealth health;
    
    private float _timeLeftBeforeSpawn;

    #region Singleton

    private static MotherShipManager _motherShipManager;

    public static MotherShipManager Instance => _motherShipManager;
    // Start is called before the first frame update

    private void Awake()
    {
        _motherShipManager = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _timeLeftBeforeSpawn = timeBeforeSpawnMinion;
        sliderSpawner.maxValue = timeBeforeSpawnMinion;
        sliderSpawner.value = timeBeforeSpawnMinion;
    }

    // Update is called once per frame
    void Update()
    {
        _timeLeftBeforeSpawn -= Time.deltaTime;
        sliderSpawner.value = _timeLeftBeforeSpawn;
        if(_timeLeftBeforeSpawn < 0)
        {
            Instantiate(prefabMinion,transformSpawnMinion.position, Quaternion.identity);
            _timeLeftBeforeSpawn = timeBeforeSpawnMinion;
        }
    }
}
