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
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }
    [Inject] private UISignals _uiSignals { get; set; }
    WaitForSeconds _faderStartDelay = new WaitForSeconds(1f);
    WaitForSeconds _faderActiveDuration = new WaitForSeconds(0.5f);
    private float _faderDuration = 0.5f;
    WaitForSeconds _fadingProcessDurationDelay;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _fadingProcessDurationDelay = new WaitForSeconds(_faderDuration);
    }

    public void CloseButton()
    {
        _coreGameSignals.onStorePanelClosed?.Invoke();
        StartCoroutine(FadeDelay());
    }

    IEnumerator FadeDelay()
    {
        //yield return _faderStartDelay;
        faderController.Fade(1, 0.5f);
        yield return _fadingProcessDurationDelay;
        _uiSignals.onClosePanel?.Invoke(UIPanels.StorePanel);
        _uiSignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
        yield return _faderActiveDuration;

        faderController.Fade(0, 0.5f);

    }
}
