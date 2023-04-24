using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Signals;
using Zenject;
using Enums;

public class EnemyHealthBarManager : HealthBarManager
{
    #region Self Variables
    #region Inject Variables
    [Inject] private EnemySignals EnemySignals { get; set; }
    [Inject] private LevelSignals LevelSignals { get; set; }

    #endregion

    #region Public Variables

    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject colliders;

    #endregion

    #region Private Variables


    #endregion

    #endregion

    #region Event Subscription
    protected override void OnEnable()
    {
        SubscribeEvents();
    }

    protected override void SubscribeEvents()
    {
        EnemySignals.onHitted += OnHitted;
    }

    protected override void UnSubscribeEvents()
    {
        EnemySignals.onHitted -= OnHitted;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    #endregion

    protected override void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    public override void OnHitted(int value, StickmanBodyPartEnums bodyPart)
    {
        base.OnHitted(value, bodyPart);
        Debug.Log(_currentHealth);
        if (_currentHealth <= 0)
        {
            LevelSignals.onEnemyDied.Invoke(transform.parent);
            EnemySignals.onDie?.Invoke(bodyPart);
            colliders.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
