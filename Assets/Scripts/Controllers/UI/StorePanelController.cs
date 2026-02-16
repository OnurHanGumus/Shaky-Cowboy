using Data.MetaData;
using Enums;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

class StorePanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] FaderController faderController;
    [SerializeField] CanvasGroup upgradesPanel;
    [SerializeField] CanvasGroup levelSelectPanel;
    #endregion
    #region Private Variables
    [Inject] private GameOptions _gameOptions { get; set; }
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }
    [Inject] private UISignals _uiSignals { get; set; }
    [Inject] private LevelSignals _levelSignals { get; set; }
    [Inject] private LoadGameDataCommand _loadCommand { get; set; }
    WaitForSeconds _faderActiveDuration;
    WaitForSeconds _fadingProcessDurationDelay;
    [SerializeField] private List<TextMeshProUGUI> levelTexts;
    [SerializeField] private List<Button> buttons;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
        InitLevelTexts();
    }

    private void InitLevelTexts()
    {
        for (int i = 0; i < levelTexts.Count; i++)
        {
            levelTexts[i].text = (i + 1).ToString();
        }
    }

    private void Init()
    {
        _fadingProcessDurationDelay = new WaitForSeconds(_gameOptions.FadingProcessDurationDelay_StorePanel);
        _faderActiveDuration = new WaitForSeconds(_gameOptions.FaderActiveDuration_StorePanel);

        for (int i = 0; i < buttons.Count; i++)
        {
            levelTexts.Add(buttons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>());

        }
    }

    public void CloseButton()
    {
        _coreGameSignals.onStorePanelClosed?.Invoke();
        StartCoroutine(FadeDelay());
        ActivateUpgrades();
    }

    public void ActivateUpgrades()
    {
        upgradesPanel.DOFade(1,0.2f);
        levelSelectPanel.DOFade(0,0.2f);
        levelSelectPanel.blocksRaycasts = false;
        upgradesPanel.blocksRaycasts = true;
    }

    public void ActivateLevels()
    {
        upgradesPanel.DOFade(0, 0.2f);
        levelSelectPanel.DOFade(1, 0.2f);
        levelSelectPanel.blocksRaycasts = true;
        upgradesPanel.blocksRaycasts = false;
        int level = _loadCommand.OnLoadGameData<int>(SaveDataEnums.Level);
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i<level)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }

    public void SelectLevel()
    {
        EventSystem currentEvent = EventSystem.current;
        Debug.Log(currentEvent.currentSelectedGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        _levelSignals.onPreviousLevelOpened?.Invoke(int.Parse(currentEvent.currentSelectedGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text));
    }

    IEnumerator FadeDelay()
    {
        faderController.Fade(1, _gameOptions.FadingProcessDurationDelay_StorePanel);
        yield return _fadingProcessDurationDelay;
        _uiSignals.onClosePanel?.Invoke(UIPanels.StorePanel);
        _uiSignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
        yield return _faderActiveDuration;

        faderController.Fade(0, _gameOptions.FadingProcessDurationDelay_StorePanel);
    }
}