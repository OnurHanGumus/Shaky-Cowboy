using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using DG.Tweening;
using System.Threading.Tasks;
using Zenject;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    [Inject] private LevelSignals LevelSignals { get; set; }

    #endregion

    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI gemText, counterText, levelText;
    #endregion
    #region Private Variables
    private int _counterValue, _counterDefaultValue = 3;
    private int _levelId = 0;

    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _levelId = LevelSignals.onGetLevelId();
        UpdateLevelText();
    }

    public void OnScoreUpdateText(ScoreTypeEnums type, int score)
    {
        if (type.Equals(ScoreTypeEnums.Gem))
        {
            gemText.text = score.ToString();
        }
    }

    private async Task Counter()
    {
        while (true)
        {
            await Task.Delay(1000);
            counterText.text = _counterValue.ToString();

            --_counterValue;
            if (_counterValue < 0)
            {
                counterText.text = "START!";
                break;
            }
        }

    }
    public void OnPlay()
    {
        _counterValue = _counterDefaultValue;
        counterText.text = _counterValue.ToString();
        Counter();
    }

    public void OnLevelSuccessful()
    {
        ++_levelId;
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level " + _levelId.ToString();
    }


    public void OnRestartLevel()
    {
    }
}
