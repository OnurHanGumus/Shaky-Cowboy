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
    [SerializeField] FaderController faderController;
    #endregion
    #region Private Variables
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }
    [Inject] private UISignals _uiSignals { get; set; }
    WaitForSeconds _faderStartDelay = new WaitForSeconds(1f);
    private float _faderDuration = 0.5f;
    WaitForSeconds _faderDurationDelay;
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _faderDurationDelay = new WaitForSeconds(_faderDuration);
    }

    public void StorePanelButton()
    {
        _coreGameSignals.onStorePanelClicked?.Invoke();
        StartCoroutine(FadeDelay());
    }

    IEnumerator FadeDelay()
    {
        yield return _faderStartDelay;
        faderController.Fade(1,0.5f);
        yield return _faderDurationDelay;
        _uiSignals.onOpenPanel?.Invoke(UIPanels.StorePanel);
        _uiSignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
        faderController.Fade(0, 0.5f);

    }
}
