using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class StartPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private FaderController faderController;
    #endregion
    #region Private Variables
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }
    [Inject] private UISignals _uiSignals { get; set; }
    [Inject] private GameOptions _gameOptions { get; set; }
    private WaitForSeconds _faderStartDelay;
    private WaitForSeconds _faderActiveDuration;
    private WaitForSeconds _fadingProcessDurationDelay;
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _fadingProcessDurationDelay = new WaitForSeconds(_gameOptions.FadingProcessDurationDelay_StorePanel);
        _faderStartDelay = new WaitForSeconds(_gameOptions.FaderStartDelay_StorePanel);
        _faderActiveDuration = new WaitForSeconds(_gameOptions.FaderActiveDuration_StorePanel);
    }

    public void StorePanelButton()
    {
        _coreGameSignals.onStorePanelClicked?.Invoke();
        StartCoroutine(FadeDelay());
    }

    IEnumerator FadeDelay()
    {
        _uiSignals.onClosePanel?.Invoke(UIPanels.StartPanel);
        yield return _faderStartDelay;
        faderController.Fade(1, _gameOptions.FadingProcessDurationDelay_StorePanel);
        yield return _fadingProcessDurationDelay;
        _uiSignals.onOpenPanel?.Invoke(UIPanels.StorePanel);
        yield return _faderActiveDuration;

        faderController.Fade(0, _gameOptions.FadingProcessDurationDelay_StorePanel);

    }
}
