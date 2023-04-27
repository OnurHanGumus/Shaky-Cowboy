using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System.Threading.Tasks;

public class PlayerRevolverManager : RevolverAbstract, IGun
{
    #region Self Variables
    #region Inject Variables
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private InputSignals InputSignals { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    #endregion
    #region Private Variables
    private bool _isDied = false;
    #endregion
    #region Properties
    #endregion
    #endregion
    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    protected override void SubscribeEvents()
    {
        PlayerSignals.onShoot += OnShoot;
        PlayerSignals.onDie += OnDie;
        base.SubscribeEvents();
    }

    private new void UnsubscribeEvents()
    {
        PlayerSignals.onShoot -= OnShoot;
        PlayerSignals.onDie -= OnDie;
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

    public void OnDie(StickmanBodyPartEnums bodyPart)
    {
        _isDied = true;
        StopAllCoroutines();
    }

    public override IEnumerator Reload()
    {
        if (!_isReloading)
        {
            _isReloading = true;
            PlayerSignals.onReload?.Invoke();
            yield return wait2_4f;

            SetRevolverPosition();

            yield return wait0_5f;
            PlayerSignals.onReloaded?.Invoke();

            _isReloading = false;
            CurrentBulletCount = AmmoCapacity;
        }
    }
}
