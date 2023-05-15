using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Controllers;
using System;

public class EnemyManager : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private LevelSignals LevelSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private EnemySignals EnemySignals { get; set; }

    #endregion

    #region Public Variables
    public bool IsDead = false;
    #endregion

    #region Serialized Variables
    [SerializeField] private EnemyRiggingControllerBase riggingController;
    [SerializeField] private EnemyAnimationControllerBase animationController;
    [SerializeField] private EnemyShootControllerBase shootController;
    [SerializeField] private Rigidbody rig;

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
        LevelSignals.onEnemyArrived?.Invoke();
    }

    #region Event Subscriptions
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onPlay += riggingController.OnPlay;
        CoreGameSignals.onPlay += shootController.OnPlay;
        CoreGameSignals.onRestart += OnRestartLevel;

        EnemySignals.onReload += OnReload;
        EnemySignals.onReload += riggingController.OnReload;
        EnemySignals.onReload += shootController.OnReload;
        EnemySignals.onReloaded += riggingController.OnReloaded;
        EnemySignals.onReloaded += shootController.OnReloaded;

        EnemySignals.onDie += OnDie;
        EnemySignals.onDie += riggingController.OnDie;
        EnemySignals.onDie += shootController.OnDie;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onPlay -= riggingController.OnPlay;
        CoreGameSignals.onPlay -= shootController.OnPlay;
        CoreGameSignals.onRestart -= OnRestartLevel;

        EnemySignals.onReload -= OnReload;
        EnemySignals.onReload -= riggingController.OnReload;
        EnemySignals.onReload -= shootController.OnReload;
        EnemySignals.onReloaded -= riggingController.OnReloaded;
        EnemySignals.onReloaded -= shootController.OnReloaded;

        EnemySignals.onDie -= OnDie;
        EnemySignals.onDie -= riggingController.OnDie;
        EnemySignals.onDie -= shootController.OnDie;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    private void Update()
    {
        if (IsDead)
        {
            return;
        }
        if ((transform.eulerAngles.x > 50 && transform.eulerAngles.x < 310) || transform.eulerAngles.z > 50 && transform.eulerAngles.z < 310)
        {
            transform.parent = LevelSignals.onGetLevelHolder().GetChild(0);
            IsDead = true;
            EnemySignals.onHitted?.Invoke(500, StickmanBodyPartEnums.Head);
        }
    }

    private void OnDie(StickmanBodyPartEnums bodyPart)
    {
        animationController.OnChangeAnimation((PlayerAnimationStates)((int)bodyPart));
    }

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
