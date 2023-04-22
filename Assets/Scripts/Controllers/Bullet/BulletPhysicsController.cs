using System;
using System.Collections;
using System.Collections.Generic;
using Components.Enemies;
using Data.MetaData;
using UnityEngine;
using Zenject;
using Enums;
using Signals;

public class BulletPhysicsController : MonoBehaviour, IPoolType
{
    #region Self Variables
    #region Inject Variables
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    #endregion
    #region Private Variables
    private Settings _mySettings;

    #endregion
    #region Properties

    #endregion
    #endregion

    [Inject]
    public void Constractor(BulletSettings bulletSettings)
    {
        _mySettings = bulletSettings.BulletCollisionDetectorSettings;
    }

    private void OnEnable()
    {
        StartCoroutine(BulletLifeTime());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable attackable))
        {
            PlayerSignals.onEnemyShooted?.Invoke(attackable);
            DespawnSignal();
            attackable.OnWeaponTriggerEnter(1);
            //GameObject particle = PoolSignals.onGetObject(PoolEnums.Explosion, transform.position);
            //particle.SetActive(false);
            //particle.transform.position = transform.position;
            //particle.SetActive(true);
        }
    }

    private IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(_mySettings.BulletLifeTime);
        DespawnSignal();
    }

    private void DespawnSignal()
    {
        gameObject.SetActive(false);
    }

    [Serializable]
    public class Settings
    {
        [SerializeField] public float BulletLifeTime = 1f;
    }
}
