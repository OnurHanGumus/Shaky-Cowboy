using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;

public class EnemyRevolverManager: RevolverAbstract, IGun
{
    #region Self Variables
    #region Inject Variables
    //[Inject] private PlayerSettings PlayerSettings { get; set; }
    [Inject] private EnemySignals EnemySignals { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private InputSignals InputSignals { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    #endregion
    #region Private Variables
    #endregion
    #region Properties
    //public int AmmoCapacity { get; set; }
    //public int CurrentBulletCount { get; set; } = 3;
    #endregion
    #endregion
    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EnemySignals.onShoot += OnShoot;
    }

    private void UnsubscribeEvents()
    {
        EnemySignals.onShoot -= OnShoot;
    }


    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    public override void OnShoot()
    {
        base.OnShoot();
    }

    public override void Reload()
    {
        base.Reload();
    }
}
