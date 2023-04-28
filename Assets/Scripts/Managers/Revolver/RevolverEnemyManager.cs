using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System.Threading.Tasks;


public class RevolverEnemyManager: RevolverManagerAbs
{
    #region Self Variables
    #region Inject Variables
    [Inject] private EnemySignals EnemySignals { get; set; }


    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables

    #endregion
    #region Protected Variables

    #endregion
    #region Private Variables
    #endregion
    #region Properties

    #endregion
    #endregion
    #region Event Subscription
    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    protected override void SubscribeEvents()
    {
        EnemySignals.onShoot += revolverShootControllerAbs.OnShoot;
        EnemySignals.onDie += revolverShootControllerAbs.OnDie;
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        EnemySignals.onShoot -= revolverShootControllerAbs.OnShoot;
        EnemySignals.onDie -= revolverShootControllerAbs.OnDie;
        base.UnsubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
}
