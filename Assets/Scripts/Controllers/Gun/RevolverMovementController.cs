using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class RevolverMovementController : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables

    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] Transform revolverTargetTransform;


    #endregion
    #region Private Variables
    private Tween _patrollingTween = null;
    private float _direction = 1f;
    #endregion
    #endregion

    public void OnPlay()
    {
        if (_patrollingTween != null && _patrollingTween.IsInitialized())
        {
            _patrollingTween.Play();
            return;
        }
        SetTween();
        //Debug.LogError(_patrollingTween);
    }

    private void SetTween()
    {
        _direction *= -1;
        _patrollingTween = revolverTargetTransform.DOPath(new Vector3[3]
        {
            new Vector3(revolverTargetTransform.position.x, revolverTargetTransform.position.y + (2.5f * _direction), revolverTargetTransform.position.z),
            new Vector3(revolverTargetTransform.position.x, revolverTargetTransform.position.y - (2.5f * _direction), revolverTargetTransform.position.z),
            new Vector3(revolverTargetTransform.position.x, revolverTargetTransform.position.y, revolverTargetTransform.position.z),
        }, 1.5f).OnComplete(() =>
        {
            _patrollingTween.Restart();
        }
        ).SetEase(Ease.Linear);
    }
    public void OnRestart()
    {
        //DOTween.Clear();
        //_patrollingTween = null;
        _patrollingTween.Pause();
    }

}
