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

class StorePanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] FaderController faderController;
    #endregion
    #region Private Variables
    [Inject] private GameOptions _gameOptions { get; set; }
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }
    [Inject] private UISignals _uiSignals { get; set; }

    WaitForSeconds _faderActiveDuration;
    WaitForSeconds _fadingProcessDurationDelay;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _fadingProcessDurationDelay = new WaitForSeconds(_gameOptions.FadingProcessDurationDelay_StorePanel);
        _faderActiveDuration = new WaitForSeconds(_gameOptions.FaderActiveDuration_StorePanel);
    }

    public void CloseButton()
    {
        _coreGameSignals.onStorePanelClosed?.Invoke();
        StartCoroutine(FadeDelay());
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

    public void Upgrade(int upgradeId) //button
    {
        _coreGameSignals.onUpgradePurchased?.Invoke((UpgradeEnums)upgradeId);
    }
}