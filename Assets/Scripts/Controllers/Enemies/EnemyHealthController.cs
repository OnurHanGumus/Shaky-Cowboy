using Enums;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

class EnemyHealthController : MonoBehaviour
{
    public int CurrentHealth = 100;
    [Inject] private EnemySignals EnemySignals { get; set; }
    [Inject] private LevelSignals LevelSignals { get; set; }
    [Inject] private ScoreSignals ScoreSignals { get; set; }
    [Inject] private EnemySettings _settings { get; set; }

    [SerializeField] private GameObject colliders;
    [SerializeField] private HealthBarManager healthBarManager;

    #region Event Subscription
    protected void OnEnable()
    {
        SubscribeEvents();
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        CurrentHealth = _settings.Health;
        healthBarManager.InitHealthValue(CurrentHealth);
    }

    protected void SubscribeEvents()
    {
        EnemySignals.onHitted += OnHitted;
    }

    protected void UnSubscribeEvents()
    {
        EnemySignals.onHitted -= OnHitted;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    #endregion

    public void OnHitted(int value, StickmanBodyPartEnums bodyPart)
    {
        CurrentHealth -= value;

        if (CurrentHealth <= 0)
        {
            LevelSignals.onEnemyDied.Invoke(transform);
            EnemySignals.onDie?.Invoke(bodyPart);
            ScoreSignals.onScoreIncrease?.Invoke(ScoreTypeEnums.Gem, value);
            colliders.SetActive(false);
            healthBarManager.gameObject.SetActive(false);
        }

        ChangeHealthBar();
    }

    private void ChangeHealthBar()
    {
        healthBarManager.ChangeHealthbar(CurrentHealth);
    }
}