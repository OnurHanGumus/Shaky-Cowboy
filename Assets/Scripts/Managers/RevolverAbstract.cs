using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;

public abstract class RevolverAbstract : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    //[Inject] private PlayerSettings PlayerSettings { get; set; }
    //[Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private InputSignals InputSignals { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] private Transform bulletPointTransform;
    #endregion
    #region Private Variables
    #endregion
    #region Properties
    public int AmmoCapacity { get; set; }
    public int CurrentBulletCount { get; set; } = 3;
    #endregion
    #endregion
    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        //PlayerSignals.onShoot += OnShoot;
    }

    private void UnsubscribeEvents()
    {
        //PlayerSignals.onShoot -= OnShoot;
    }


    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    public virtual void OnShoot()
    {
        GameObject bullet = PoolSignals.onGetObject?.Invoke(PoolEnums.Bullet, transform.position);
        bullet.transform.position = bulletPointTransform.position;
        bullet.transform.eulerAngles = transform.eulerAngles;
        bullet.SetActive(true);
        //AudioSignals.onPlaySound?.Invoke(SoundEnums.Fire);
    }

    public virtual void Reload()
    {
    }
}
