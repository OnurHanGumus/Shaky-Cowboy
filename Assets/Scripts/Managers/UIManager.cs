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
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private AudioSignals AudioSignals { get; set; }
        [Inject] private SaveSignals SaveSignals { get; set; }
        [Inject] private ScoreSignals ScoreSignals { get; set; }
        [Inject] private UISignals UISignals { get; set; }
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
        [SerializeField] private GameOverPanelController gameOverPanelController;
        [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private HighScorePanelController highScorePanelController;

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
            UISignals.onOpenPanel += OnOpenPanel;
            UISignals.onClosePanel += OnClosePanel;
            CoreGameSignals.onPlay += OnPlay;
            CoreGameSignals.onLevelFailed += OnLevelFailed;
            CoreGameSignals.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.onLevelSuccessful += levelPanelController.OnLevelSuccessful;
            CoreGameSignals.onRestart += levelPanelController.OnRestartLevel;
        }

        private void UnsubscribeEvents()
        {
            UISignals.onOpenPanel -= OnOpenPanel;
            UISignals.onClosePanel -= OnClosePanel;
            CoreGameSignals.onPlay -= OnPlay;
            CoreGameSignals.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.onLevelSuccessful -= levelPanelController.OnLevelSuccessful;
            CoreGameSignals.onRestart -= levelPanelController.OnRestartLevel;
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
            UISignals.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnLevelFailed()
        {
            UISignals.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.FailPanel);
            //gameOverPanelController.ShowThePanel();
        }

        private void OnLevelSuccessful()
        {
            UISignals.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.WinPanel);
        }

        public void Play()
        {
            CoreGameSignals.onPlay?.Invoke();
            UISignals.onClosePanel?.Invoke(UIPanels.StorePanel);
            UISignals.onClosePanel?.Invoke(UIPanels.StartPanel);
        }

        public void NextLevel()
        {
            CoreGameSignals.onNextLevel?.Invoke();
            UISignals.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            CoreGameSignals.onRestart?.Invoke();
            UISignals.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void PauseButton()
        {
            UISignals.onOpenPanel?.Invoke(UIPanels.PausePanel);
            Time.timeScale = 0f;
        }

        public void HighScoreButton()
        {
            UISignals.onOpenPanel?.Invoke(UIPanels.HighScorePanel);
            UISignals.onClosePanel?.Invoke(UIPanels.StartPanel);
        }
        public void OptionsButton()
        {
            UISignals.onOpenPanel?.Invoke(UIPanels.OptionsPanel);
            //Debug.Log("Clicked");
        }

        public void StoreButton()
        {
            if (!_isStorePanelOpened)
            {
                UISignals.onOpenPanel?.Invoke(UIPanels.StorePanel);
            }
            else
            {
                UISignals.onClosePanel?.Invoke(UIPanels.StorePanel);
            }
            _isStorePanelOpened = !_isStorePanelOpened;

        }
    }
}