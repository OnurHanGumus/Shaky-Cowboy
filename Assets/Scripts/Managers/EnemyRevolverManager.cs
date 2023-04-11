using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System.Threading.Tasks;

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

    protected override void SubscribeEvents()
    {
        EnemySignals.onShoot += OnShoot;
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        EnemySignals.onShoot -= OnShoot;
        base.UnsubscribeEvents();
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

    //public override async Task Reload()
    //{
    //    EnemySignals.onReload?.Invoke();
    //    await base.Reload();
    //    await Task.Delay(100);

    //    EnemySignals.onReloaded?.Invoke();
    //}
    public override IEnumerator Reload()
    {
        if (!_isReloading)
        {
            _isReloading = true;
            EnemySignals.onReload?.Invoke();
            yield return new WaitForSeconds(2);
            transform.parent = base.playerTransform;
            transform.localPosition = revolverInitializePos;
            transform.localEulerAngles = revolverInitializeRot;

            yield return new WaitForSeconds(0.5f);
            EnemySignals.onReloaded?.Invoke();

            _isReloading = false;
            CurrentBulletCount = AmmoCapacity;
        }
    }
}
