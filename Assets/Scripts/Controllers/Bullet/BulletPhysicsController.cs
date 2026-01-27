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
    [Inject] private PlayerSignals _playerSignals { get; set; }
    [Inject] private AudioSignals _audioSignals { get; set; }
    [Inject] private PlayerSettings _playerSettings { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] private ParticleActivenessController particleActivenessController;
    [SerializeField] private BulletSpriteController bulletSpriteController;
    [SerializeField] private Rigidbody rig;
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
            EnemyHitted(attackable);
        }
        else if (other.TryGetComponent(out IReplaceable replaceable))
        {
            ReplaceableHitted(replaceable);
        }
    }

    private void ReplaceableHitted(IReplaceable replaceable)
    {
        replaceable.OnShooted(rig.linearVelocity);
        Hitted();
        _audioSignals.onPlaySound?.Invoke(AudioSoundEnums.HitReplaceable);
    }

    private void EnemyHitted(IAttackable attackable)
    {
        _playerSignals.onEnemyShooted?.Invoke(attackable);
        Hitted();
        attackable.OnWeaponTriggerEnter(_playerSettings.Settings[UpgradeEnums.DamageMultiplierUpgrade]);
        _audioSignals.onPlaySound?.Invoke(AudioSoundEnums.HitStickMan);
    }

    private IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(_mySettings.BulletLifeTime);
        Despawn();
    }

    private void Hitted()
    {
        bulletSpriteController.Disable();
        rig.linearVelocity = Vector3.zero;
        particleActivenessController.Disable();
    }

    private void Despawn()
    {
        gameObject.SetActive(false);

    }

    [Serializable]
    public class Settings
    {
        [SerializeField] public float BulletLifeTime = 1f;
    }
}
