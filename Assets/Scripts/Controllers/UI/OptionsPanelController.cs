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
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private AudioSignals AudioSignals { get; set; }
    [Inject] private SaveSignals SaveSignals { get; set; }
    [Inject] private ScoreSignals ScoreSignals { get; set; }
    [Inject] private UISignals UISignals { get; set; }
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
        _audioSourceActiveness = SaveSignals.onGetSoundState(SaveLoadStates.SoundState, SaveFiles.GameOptions) == 1;
        soundToggle.isOn = _audioSourceActiveness;
        SetAudioSource();
    }
    public void OnValueChanged()
    {
        SaveSignals.onChangeSoundState?.Invoke(soundToggle.isOn ? 1 : 0, SaveLoadStates.SoundState, SaveFiles.GameOptions);
        _audioSourceActiveness = !_audioSourceActiveness;
        SetAudioSource();
    }
    public void CloseOptionsPanel()
    {
        UISignals.onClosePanel?.Invoke(UIPanels.OptionsPanel);
    }
    private void SetAudioSource()
    {
        //audioSource.enabled = _audioSourceActiveness;
        //AudioListener.pause = _audioSourceActiveness;
        AudioListener.volume = _audioSourceActiveness ? 1 : 0;
    }


}
