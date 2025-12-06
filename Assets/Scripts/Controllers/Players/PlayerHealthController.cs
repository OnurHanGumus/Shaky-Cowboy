using Enums;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

class PlayerHealthController : MonoBehaviour
{
    public int CurrentHealth = 100;
    [Inject] private PlayerSettings _settings { get; set; }
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }

    [SerializeField] private HealthBarManager healthBarManager;
    private bool _isLevelSuccessful = false;

    #region Event Subscription
    protected void OnEnable()
    {
        SubscribeEvents();
        UpdateHealth();

    }

    protected void SubscribeEvents()
    {
        PlayerSignals.onHitted += OnHitted;
        CoreGameSignals.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.onRestart += OnRestart;

    }

    protected void UnSubscribeEvents()
    {
        PlayerSignals.onHitted -= OnHitted;
        CoreGameSignals.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.onRestart -= OnRestart;

    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    #endregion

    private void UpdateHealth()
    {
        CurrentHealth = _settings.Health;
        healthBarManager.InitHealthValue(CurrentHealth);
    }

    public void OnHitted(int value, StickmanBodyPartEnums bodyPart)
    {
        CurrentHealth -= value;

        if (_isLevelSuccessful)
        {
            return;
        }
        if (CurrentHealth <= 0)
        {
            PlayerSignals.onDie?.Invoke(bodyPart);
            CoreGameSignals.onLevelFailed?.Invoke();
        }

        ChangeHealthBar();
    }

    private void ChangeHealthBar()
    {
        healthBarManager.ChangeHealthbar(CurrentHealth);
    }

    public void OnLevelSuccessful()
    {
        _isLevelSuccessful = true;
    }

    protected void OnRestart()
    {
        _isLevelSuccessful = false;
        CurrentHealth = 100;
    }
}