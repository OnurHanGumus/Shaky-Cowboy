using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OptionsPanelController : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private UISignals _uiSignals { get; set; }
    [Inject] private SaveGameCommand _saveCommand { get; set; }
    [Inject] private LoadGameDataCommand _loadCommand { get; set; }
    #endregion

    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private AudioSource audioSource;
    #endregion
    #region Private Variables
    private bool _audioSourceActiveness;
    #endregion
    #endregion

    private void Start()
    {
        if (!_loadCommand.CheckIfKeyInitialized(SaveDataEnums.Music))
        {
            _saveCommand.OnSaveData<int>(SaveDataEnums.Music, 1);
        }

        _audioSourceActiveness = _loadCommand.OnLoadGameData<int>(SaveDataEnums.Music) == 1;
        soundToggle.isOn = _audioSourceActiveness;
        SetAudioSource();
    }

    public void OnValueChanged()
    {
        _saveCommand.OnSaveData<int>(SaveDataEnums.Music, soundToggle.isOn ? 1 : 0);
        _audioSourceActiveness = !_audioSourceActiveness;
        SetAudioSource();
    }

    public void CloseOptionsPanel()
    {
        _uiSignals.onClosePanel?.Invoke(UIPanels.OptionsPanel);
    }

    private void SetAudioSource()
    {
        audioSource.mute = !_audioSourceActiveness;
    }
}
