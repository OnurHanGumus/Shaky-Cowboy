using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System.Threading.Tasks;

public class RevolverShootControllerEnemy: RevolverShootControllerAbs, IGun
{
    #region Self Variables
    #region Inject Variables
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
    #endregion
    #endregion
    #region Event Subscription

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    #endregion

    public override void OnShoot()
    {
        base.OnShoot();
    }

    public override void OnDie(StickmanBodyPartEnums bodyPart)
    {
        base.OnDie(bodyPart);
        _isDied = true;
        StopAllCoroutines();
    }

    public override void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    public override IEnumerator ReloadCoroutine()
    {
        if (!_isReloading)
        {
            _isReloading = true;
            RevolverOnHand();

            EnemySignals.onReload?.Invoke();
            yield return wait2_4f;

            SetRevolverPosition();
            yield return wait0_5f;
            EnemySignals.onReloaded?.Invoke();

            _isReloading = false;
            CurrentBulletCount = AmmoCapacity;
        }
    }
}
