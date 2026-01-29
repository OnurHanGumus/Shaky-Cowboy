using Controllers;
using Enums;
using Signals;
using UnityEngine;
using Data.UnityObject;
using Data.ValueObject;
using Zenject;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables
        #region Injected Variables
        [Inject] private CoreGameSignals _coreGameSignals { get; set; }
        [Inject] private UISignals _uISignals { get; set; }
        #endregion
        #region Public Variables
        #region Array struct
        public Dictionary<ScoreTypeEnums, TextMeshProUGUI> TextDictionary;

        [Serializable]
        public struct TextObjects
        {
            public ScoreTypeEnums ScoreType;
            public TextMeshProUGUI Text;
        }
        [SerializeField] TextObjects[] textInspectorDictionary;
        #endregion
        #endregion
        #region Serialized Variables

        [SerializeField] private UIPanelActivenessController uiPanelController;
        [SerializeField] private LevelPanelController levelPanelController;

        #endregion
        #region Private Variables

        private UIData _data;
        private bool _isStorePanelOpened = false;

        #endregion
        #endregion

        #region Event Subscriptions
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
            TextDictionary = new Dictionary<ScoreTypeEnums, TextMeshProUGUI>();
            foreach (var i in textInspectorDictionary)
            {
                TextDictionary.Add(i.ScoreType, i.Text);
            }
        }

        private UIData GetData() => Resources.Load<CD_UI>("Data/CD_UI").Data;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _uISignals.onOpenPanel += OnOpenPanel;
            _uISignals.onClosePanel += OnClosePanel;
            _coreGameSignals.onPlay += OnPlay;
            _coreGameSignals.onLevelFailed += OnLevelFailed;
            _coreGameSignals.onLevelSuccessful += OnLevelSuccessful;
            _coreGameSignals.onLevelSuccessful += levelPanelController.OnLevelSuccessful;
            _coreGameSignals.onRestart += levelPanelController.OnRestartLevel;
        }

        private void UnsubscribeEvents()
        {
            _uISignals.onOpenPanel -= OnOpenPanel;
            _uISignals.onClosePanel -= OnClosePanel;
            _coreGameSignals.onPlay -= OnPlay;
            _coreGameSignals.onLevelFailed -= OnLevelFailed;
            _coreGameSignals.onLevelSuccessful -= OnLevelSuccessful;
            _coreGameSignals.onLevelSuccessful -= levelPanelController.OnLevelSuccessful;
            _coreGameSignals.onRestart -= levelPanelController.OnRestartLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenMenu(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.CloseMenu(panelParam);
        }

        private void OnPlay()
        {
            _uISignals.onClosePanel?.Invoke(UIPanels.StartPanel);
            _uISignals.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnLevelFailed()
        {
            _uISignals.onClosePanel?.Invoke(UIPanels.LevelPanel);
            _uISignals.onOpenPanel?.Invoke(UIPanels.FailPanel);
            //gameOverPanelController.ShowThePanel();
        }

        private void OnLevelSuccessful()
        {
            _uISignals.onClosePanel?.Invoke(UIPanels.LevelPanel);
            _uISignals.onOpenPanel?.Invoke(UIPanels.WinPanel);
        }

        public void Play()
        {
            _coreGameSignals.onPlay?.Invoke();
            _uISignals.onClosePanel?.Invoke(UIPanels.StorePanel);
            _uISignals.onClosePanel?.Invoke(UIPanels.StartPanel);
        }

        public void NextLevel()
        {
            _coreGameSignals.onNextLevel?.Invoke();
            _uISignals.onClosePanel?.Invoke(UIPanels.WinPanel);
            _uISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            _coreGameSignals.onRestart?.Invoke();
            _uISignals.onClosePanel?.Invoke(UIPanels.FailPanel);
            _uISignals.onClosePanel?.Invoke(UIPanels.LevelPanel);
            _uISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);

        }

        public void PauseButton()
        {
            _uISignals.onOpenPanel?.Invoke(UIPanels.PausePanel);
            Time.timeScale = 0f;
        }

        public void HighScoreButton()
        {
            _uISignals.onClosePanel?.Invoke(UIPanels.StartPanel);
        }

        public void BulletTimeButton()
        {
            _coreGameSignals.onBulletTimeActivated?.Invoke();
        }

        public void OptionsButton()
        {
            _uISignals.onOpenPanel?.Invoke(UIPanels.OptionsPanel);
            //Debug.Log("Clicked");
        }

        public void StoreButton()
        {
            if (!_isStorePanelOpened)
            {
                _uISignals.onOpenPanel?.Invoke(UIPanels.StorePanel);
            }
            else
            {
                _uISignals.onClosePanel?.Invoke(UIPanels.StorePanel);
            }
            _isStorePanelOpened = !_isStorePanelOpened;

        }
    }
}