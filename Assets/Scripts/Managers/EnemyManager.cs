using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Controllers;

public class EnemyManager : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private InputSignals InputSignals { get; set; }
    [Inject] private RevolverSignals RevolverSignals { get; set; }
    [Inject] private EnemySignals EnemySignals { get; set; }

    #endregion

    #region Public Variables

    #endregion

    #region Serialized Variables
    [SerializeField] private EnemyRiggingController riggingController;
    [SerializeField] private EnemyAnimationController animationController;
    [SerializeField] private EnemyShootController shootController;

    #endregion

    #region Private Variables
    #endregion

    #endregion
    public void Construct(PoolSignals poolSignals, CoreGameSignals coreGameSignals)
    {
        PoolSignals = poolSignals;
        CoreGameSignals = coreGameSignals;
    }

    #region Event Subscriptions
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onPlay += riggingController.OnPlay;
        CoreGameSignals.onRestart += OnRestartLevel;

        EnemySignals.onReload += OnReload;
        EnemySignals.onReload += riggingController.OnReload;
        EnemySignals.onReload += shootController.OnReload;
        EnemySignals.onReloaded += riggingController.OnReloaded;

    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onPlay -= riggingController.OnPlay;
        CoreGameSignals.onRestart -= OnRestartLevel;

        EnemySignals.onReload -= OnReload;
        EnemySignals.onReload -= riggingController.OnReload;
        EnemySignals.onReload -= shootController.OnReload;
        EnemySignals.onReloaded -= riggingController.OnReloaded;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion
    private void OnReload()
    {
        animationController.OnChangeAnimation(PlayerAnimationStates.Reload);
    }
    private void OnRestartLevel()
    {
        gameObject.SetActive(false);
    }

    public class Factory : PlaceholderFactory<EnemyManager>, IPool 
    {
        GameObject IPool.OnCreate()
        {
            return base.Create().gameObject;
        }
    }
}
