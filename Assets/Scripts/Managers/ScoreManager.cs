using System;
using System.Collections.Generic;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;
using Enums;
using Zenject;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables
        #region Injected Variables
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private LevelSignals LevelSignals { get; set; }
        [Inject] private SaveSignals SaveSignals { get; set; }
        [Inject] private UISignals UISignals { get; set; }
        [Inject] private ScoreSignals ScoreSignals { get; set; }
        #endregion

        #region Public Variables


        #endregion

        #region Serialized Variables


        #endregion

        #region Private Variables
        private Dictionary<ScoreTypeEnums,int> _scoreTypeDictionary; 
        private ScoreData _data;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _scoreTypeDictionary = new Dictionary<ScoreTypeEnums, int>();
            _scoreTypeDictionary.Add(ScoreTypeEnums.Gem, GetGem());
            UISignals.onSetChangedText?.Invoke(ScoreTypeEnums.Gem, _scoreTypeDictionary[ScoreTypeEnums.Gem]);
        }

        private int GetGem()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Gem") ? ES3.Load<int>("Gem") : 0;
        }
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.onScoreIncrease += OnScoreIncrease;
            ScoreSignals.onScoreDecrease += OnScoreDecrease;
            ScoreSignals.onGetGem += OnGetGem;
            CoreGameSignals.onNextLevel += OnNextLevel;
            CoreGameSignals.onRestart += OnRestartLevel;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.onScoreIncrease -= OnScoreIncrease;
            ScoreSignals.onScoreDecrease -= OnScoreDecrease;
            ScoreSignals.onGetGem -= OnGetGem;
            CoreGameSignals.onNextLevel -= OnNextLevel;
            CoreGameSignals.onRestart -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnScoreIncrease(ScoreTypeEnums type, int amount)
        {
            _scoreTypeDictionary[type] += amount;
            UISignals.onSetChangedText?.Invoke(type, _scoreTypeDictionary[type]);
        }

        private void OnScoreDecrease(ScoreTypeEnums type, int amount)
        {
            _scoreTypeDictionary[type] -= amount;
            SaveSignals.onSave(_scoreTypeDictionary[type], (SaveLoadStates)Enum.Parse(typeof(SaveLoadStates), type.ToString()), SaveFiles.SaveFile);
            UISignals.onSetChangedText?.Invoke(type, _scoreTypeDictionary[type]);
        }

        private void OnNextLevel()
        {
            SaveSignals.onSave(_scoreTypeDictionary[ScoreTypeEnums.Gem], SaveLoadStates.Gem, SaveFiles.SaveFile);
        }

        private int OnGetGem()
        {
            return _scoreTypeDictionary[ScoreTypeEnums.Gem];
        }

        private void OnRestartLevel()
        {

        }
    }
}