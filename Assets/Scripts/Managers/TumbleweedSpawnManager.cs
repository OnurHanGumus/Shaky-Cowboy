using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Zenject;
using Signals;
using System.Threading.Tasks;
using System;
using Random = UnityEngine.Random;
using Data.MetaData;
using Data.UnityObject;
using Data.ValueObject;

public class TumbleweedSpawnManager: ITickable, IInitializable
{
    #region Self Variables
    #region Injected Variables
    [Inject] private TumbleweedSpawnSettings TumbleweedSpawnSettings { get; set; }

    #endregion

    #region Serialized Variables
    //[SerializeField] private bool isStarted = false;

    #endregion
    #region Private Variables
    private List<Vector3> _spawnPoints;
    private PoolSignals PoolSignals { get; set; }
    private CoreGameSignals CoreGameSignals { get; set; }
    private LevelSignals LevelSignals { get; set; }
    private int _levelId = 0;
    private float _timer;
    private Settings _mySettings;

    #endregion
    #endregion

    public TumbleweedSpawnManager(PoolSignals poolSignals, CoreGameSignals coreGameSignals, LevelSignals levelSignals)
    {
        PoolSignals = poolSignals;
        CoreGameSignals = coreGameSignals;
        LevelSignals = levelSignals;
        Awake();
    }

    private void Awake()
    {
        Init();
        SubscribeEvents();
    }

    private void Init()
    {
        _spawnPoints = new List<Vector3>() { new Vector3(-4.41f, -1.901f, -1.51f), new Vector3(-4.41f, -1.901f, 1.91f)};

    }

    public void Initialize()
    {
        //Start
        _mySettings = TumbleweedSpawnSettings.SpawnSettings;

    }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();

    }

    private void SubscribeEvents()
    {
        //CoreGameSignals.onPlay += OnPlay;
    }

    private void UnsubscribeEvents()
    {
        //CoreGameSignals.onPlay -= OnPlay;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    public void Tick()
    {
        _timer -= (Time.deltaTime);
        if (_timer > 0)
        {
            return;
        }
        
        GameObject poolObject = PoolSignals.onGetObject(PoolEnums.Tumbleweed, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
        poolObject.SetActive(true);
        _timer = Random.Range(_mySettings.SpawnDelayMin, _mySettings.SpawnDelayMax);
    }

    [Serializable]
    public class Settings
    {
        [SerializeField]
        public float SpawnDelayMin = 3, SpawnDelayMax = 10;
    }
}
