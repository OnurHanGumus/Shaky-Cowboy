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
    #endregion
    #endregion

    public void OnPlay()
    {
        if (_patrollingTween != null && _patrollingTween.IsInitialized())
        {
            _patrollingTween.Play();
            return;
        }

        _patrollingTween = revolverTargetTransform.DOPath(new Vector3[2] 
        { 
            new Vector3(revolverTargetTransform.position.x, 2.5f, revolverTargetTransform.position.z), 
            new Vector3(revolverTargetTransform.position.x, -1.5f, revolverTargetTransform.position.z) 
        }, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //Debug.LogError(_patrollingTween);
    }
    public void OnRestart()
    {
        //DOTween.Clear();
        //_patrollingTween = null;
        _patrollingTween.Pause();
    }

}
