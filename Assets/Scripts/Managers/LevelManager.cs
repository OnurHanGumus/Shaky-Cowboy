using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables
        #region Injected Variables
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private LevelSignals LevelSignals { get; set; }
        [Inject] private SaveGameCommand _saveCommand { get; set; }
        [Inject] private LoadGameDataCommand _loadCommand { get; set; }
        [Inject] DiContainer Container;
        #endregion
        #region Public Variables


        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private GameObject levelHolder;
        [SerializeField] private ClearActiveLevelCommand levelClearer;

        #endregion

        #region Private Variables

        private int _reachedMaksimumLevelId;
        private int _currentLevelId;
        private int _currentModdedLevel = 0;

        private int _killedEnemyCount = 0;
        private int _currentLevelEnemyCount;

        private bool _isPlayerDead = false;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _reachedMaksimumLevelId = _loadCommand.OnLoadGameData<int>(SaveDataEnums.Level) + 1;
            _currentLevelId = _reachedMaksimumLevelId;
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.onNextLevel += OnNextLevel;
            CoreGameSignals.onRestart += OnRestartLevel;
            LevelSignals.onGetLevelId += OnGetLevelId;
            LevelSignals.onGetCurrentModdedLevel += OnGetModdedLevel;
            LevelSignals.onEnemyArrived += OnEnemyArrived;
            LevelSignals.onEnemyDied += OnEnemyDie;
            LevelSignals.onGetLevelHolder += OnGetLevelHolder;
            LevelSignals.onPreviousLevelOpened += OnPreviousLevelOpened;

            PlayerSignals.onDie += OnPlayerDie;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.onNextLevel -= OnNextLevel;
            CoreGameSignals.onRestart -= OnRestartLevel;
            LevelSignals.onGetLevelId -= OnGetLevelId;
            LevelSignals.onGetCurrentModdedLevel -= OnGetModdedLevel;
            LevelSignals.onEnemyArrived -= OnEnemyArrived;
            LevelSignals.onEnemyDied -= OnEnemyDie;
            LevelSignals.onGetLevelHolder -= OnGetLevelHolder;
            LevelSignals.onPreviousLevelOpened -= OnPreviousLevelOpened;


            PlayerSignals.onDie -= OnPlayerDie;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void Start()
        {
            OnInitializeLevel(_reachedMaksimumLevelId);
        }

        private void OnNextLevel()
        {
            if (_currentLevelId!=_reachedMaksimumLevelId)
            {
                _currentLevelId = _reachedMaksimumLevelId;
            }
            else
            {
                _reachedMaksimumLevelId++;
                _currentLevelId = _reachedMaksimumLevelId;
                _saveCommand.OnSaveData(SaveDataEnums.Level, _reachedMaksimumLevelId);

            }
            CoreGameSignals.onClearActiveLevel?.Invoke();
            CoreGameSignals.onRestart?.Invoke();
        }

        private void OnPreviousLevelOpened(int levelId)
        {
            _currentLevelId = levelId;
            CoreGameSignals.onClearActiveLevel?.Invoke();
            CoreGameSignals.onRestart?.Invoke();
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.onClearActiveLevel?.Invoke();
            CoreGameSignals.onReset?.Invoke();
            _killedEnemyCount = 0;
            _currentLevelEnemyCount = 0;
            _isPlayerDead = false;
            CoreGameSignals.onLevelInitialize?.Invoke();
            OnInitializeLevel(_currentLevelId);

        }

        private int OnGetLevelId()
        {
            Debug.Log(_currentModdedLevel);
            return _currentModdedLevel;
        }

        private void OnInitializeLevel(int levelId)
        {

            UnityEngine.Object[] Levels = Resources.LoadAll("Levels");
            int newLevelId = levelId % Levels.Length;
            _currentModdedLevel = newLevelId;
            if (_currentModdedLevel == 0)
            {
                _currentModdedLevel = 1;
            }
            //levelLoader.InitializeLevel((GameObject)Levels[newLevelId], levelHolder.transform);
            GameObject level = Container.InstantiatePrefabResource("Levels/" + (_currentModdedLevel + 1).ToString()); /*episodeFactory.Create().gameObject;*/
            level.transform.parent = levelHolder.transform;
            level.transform.localPosition = Vector3.zero;
            GameObject levelDesign = Container.InstantiatePrefabResource("LevelDesign/" + 1.ToString()); /*episodeFactory.Create().gameObject;*/
            levelDesign.transform.parent = levelHolder.transform;
            levelDesign.transform.localPosition = Vector3.zero;

        }

        private int OnGetModdedLevel()
        {
            return _currentModdedLevel;
        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        private void OnEnemyArrived()
        {
            ++_currentLevelEnemyCount;
        }

        private void OnEnemyDie(Transform diedEnemy)
        {
            if (_isPlayerDead)
            {
                return;
            }

            ++_killedEnemyCount;
            if (_killedEnemyCount == _currentLevelEnemyCount)
            {
                CoreGameSignals.onLevelSuccessful?.Invoke();
                LevelSignals.onLastEnemyDied?.Invoke(diedEnemy);
            }
        }

        private void OnPlayerDie(StickmanBodyPartEnums bodyPart)
        {
            _isPlayerDead = true;
        }

        private Transform OnGetLevelHolder()
        {
            return levelHolder.transform;
        }
    }
}