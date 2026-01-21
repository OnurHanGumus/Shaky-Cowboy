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

    #endregion

    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private UIManager manager;
    #endregion
    #region Private Variables

    #endregion
    #endregion
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        UpdateText();
    }

    public void OnLevelSuccessful()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        levelText.text = "Level " + LevelSignals.onGetLevelId();
    }

    public void OnRestartLevel()
    {
        UpdateText();
    }
}
