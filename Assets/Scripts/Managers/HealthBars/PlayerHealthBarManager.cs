using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Signals;
using Zenject;
using Enums;

public class PlayerHealthBarManager : HealthBarManager
{
    #region Self Variables
    #region Inject Variables
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private LevelSignals LevelSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }

    #endregion

    #region Public Variables

    #endregion

    #region Serialized Variables

    #endregion

    #region Private Variables
    private bool _isLevelSuccessful = false;

    #endregion

    #endregion

    #region Event Subscription
    protected override void OnEnable()
    {
        SubscribeEvents();
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        PlayerSignals.onHitted += OnHitted;
        CoreGameSignals.onLevelSuccessful += OnLevelSuccessful;
    }

    protected override void UnSubscribeEvents()
    {
        base.UnSubscribeEvents();
        PlayerSignals.onHitted -= OnHitted;
        CoreGameSignals.onLevelSuccessful -= OnLevelSuccessful;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnHitted(int value, StickmanBodyPartEnums bodyPart)
    {
        if (_isLevelSuccessful)
        {
            return;
        }
        base.OnHitted(value, bodyPart);
        if (_currentHealth <= 0)
        {
            PlayerSignals.onDie?.Invoke(bodyPart);
            CoreGameSignals.onLevelFailed?.Invoke();
            //playerTransform.gameObject.SetActive(false);
        }
    }

    public void OnLevelSuccessful()
    {
        _isLevelSuccessful = true;
    }

    protected override void OnRestart()
    {
        base.OnRestart();
        _isLevelSuccessful = false;
    }
}
