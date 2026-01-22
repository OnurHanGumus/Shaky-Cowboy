using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RichTap;
using RichTap.Source;
using Zenject;
using RichTap.Common;

public class HapticManager : IHaptic, Zenject.IInitializable
{
    private RichtapPresetEffect _preset = new RichtapPresetEffect();
    [Inject] HapticSettings _hapticSettings { get; set; }
    //[Inject] HapticModesSettings _hapticModesSettings { get; set; }
    [Inject] SaveGameCommand _saveCommand { get; set; }
    [Inject] LoadGameDataCommand _loadCommand { get; set; }
    private int _hapticMode = 0;
    public int HapticMode
    {
        get { return _hapticMode; }
        set
        {
            _hapticMode = value;
            //_saveCommand.OnSaveData<int>(SaveDataEnums.HapticMode, HapticMode, SaveFileEnums.GameOptions.ToString());
        }
    }


    public void Play()
    {
        if (_preset != null)
        {
            _preset.Play();
        }
    }

    public void SetAmplitude(float value)
    {
        if (_preset == null)
        {
            return;
        }
        _preset.SetAmplitude((int)value);
    }

    public void SetPreset(RichtapPreset value)
    {
        _preset.SetPreset(value);
    }

    public void CheckBeforePlay(HapticEnums hapticEnum)
    {
        //foreach (var i in _hapticModesSettings.AllowedHaptics[(HapticUiEnums)HapticMode])
        //{
        //    if (i == hapticEnum)
        //    {
        //        PlayArrangements(hapticEnum);
        //        break;
        //    }
        //}

        PlayArrangements(hapticEnum);

    }

    public void PlayArrangements(HapticEnums hapticEnum)
    {
        _preset.SetAmplitude((int)_hapticSettings.HapticDictionary[hapticEnum].Amplitude);
        _preset.SetPreset(_hapticSettings.HapticDictionary[hapticEnum].Preset);
        Play();
    }

    public void Initialize()
    {
        //if (_loadCommand.CheckIfKeyInitialized(SaveDataEnums.HapticMode, SaveFileEnums.GameOptions.ToString()))
        //{
        //    HapticMode = _loadCommand.OnLoadGameData<int>(SaveDataEnums.HapticMode, SaveFileEnums.GameOptions.ToString());
        //}
        //else
        //{
        //    HapticMode = 2;
        //}
    }
}
