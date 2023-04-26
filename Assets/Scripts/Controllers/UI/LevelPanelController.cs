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
using Managers;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    [Inject] private LevelSignals LevelSignals { get; set; }
    [Inject] private ScoreSignals ScoreSignals { get; set; }

    #endregion

    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI gemText, counterText, levelText;
    [SerializeField] private UIManager manager;
    #endregion
    #region Private Variables
    private int _counterValue, _counterDefaultValue = 3;
    private int _levelId = 0, _gemCount = 0;

    #endregion
    #endregion
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _levelId = LevelSignals.onGetLevelId();
        _gemCount = ScoreSignals.onGetGem();

        UpdateText();
    }

    public void OnScoreUpdateText(ScoreTypeEnums type, int score)
    {
        manager.TextDictionary[type].text = score.ToString();
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
        UpdateText();
    }

    private void UpdateText()
    {
        levelText.text = "Level " + _levelId.ToString();
        gemText.text = _gemCount.ToString(); ;
    }


    public void OnRestartLevel()
    {
    }
}
