using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using System;

public class RevolverMovementController : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] Transform revolverTargetTransform;
    [SerializeField] private float amplitude = 3f;
    [SerializeField] private float targetPosY = -2.96f;
    [SerializeField] private float movementDurationDefault = 1.5f;
    [SerializeField] private float movementDurationBulletTime = 1f;
    [SerializeField] private Ease movementEase = Ease.Linear;

    #endregion
    #region Private Variables
    private Tween _patrollingTween = null;
    private float _direction = 1f;
    private float _movementDuration = 1.5f;

    #endregion
    #endregion

    private void Awake()
    {
        Init();
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _coreGameSignals.onBulletTimeAnimationCompleted += OnBulletTimeAnimationCompleted;
    }

    private void OnBulletTimeAnimationCompleted()
    {
        if (_coreGameSignals.isBulletTimeActivated())
        {
            _movementDuration = movementDurationBulletTime;
        }
        else
        {
            _movementDuration = movementDurationDefault;
        }
    }

    private void Init()
    {
        _movementDuration = movementDurationDefault;
        revolverTargetTransform.position = new Vector3(revolverTargetTransform.position.x, targetPosY, revolverTargetTransform.position.z);
    }

    public void OnPlay()
    {
        if (_patrollingTween != null && _patrollingTween.IsInitialized())
        {
            _patrollingTween.Play();
            return;
        }
        SetTween();
    }

    private void SetTween()
    {
        //_direction *= -1;
        _patrollingTween = revolverTargetTransform.DOPath(new Vector3[3]
        {
            new Vector3(revolverTargetTransform.position.x, targetPosY + (amplitude * _direction), revolverTargetTransform.position.z),
            new Vector3(revolverTargetTransform.position.x, targetPosY - (amplitude * _direction), revolverTargetTransform.position.z),
            new Vector3(revolverTargetTransform.position.x, targetPosY, revolverTargetTransform.position.z),
        }, _movementDuration).OnComplete(() =>
        {
            //_patrollingTween.Restart();
            SetTween();
        }
        ).SetEase(movementEase);
    }

    public void OnRestart()
    {
        _movementDuration = movementDurationDefault;
        _patrollingTween.Pause();
    }
}
