using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System.Threading.Tasks;

public class RevolverShootControllerPlayer : RevolverShootControllerAbs, IGun
{
    #region Self Variables
    #region Inject Variables
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private PlayerSettings _settings { get; set; }
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
    private void OnEnable()
    {
        wait2_4f = new WaitForSeconds(2.4f / _settings.ReloadSpeed);
        wait0_5f = new WaitForSeconds(0.5f / _settings.ReloadSpeed);
        AmmoCapacity = _settings.MagazineCapacity;
        CurrentBulletCount = AmmoCapacity;
    }

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
