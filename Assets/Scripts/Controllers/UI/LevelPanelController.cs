using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using DG.Tweening;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI scoreText;
    #endregion
    #region Private Variables


    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {


    }
    public void OnScoreUpdateText(ScoreTypeEnums type, int score)
    {
        if (type.Equals(ScoreTypeEnums.Gem))
        {
            scoreText.text = score.ToString();
        }
    }

    public void OnRestartLevel()
    {
    }
}
