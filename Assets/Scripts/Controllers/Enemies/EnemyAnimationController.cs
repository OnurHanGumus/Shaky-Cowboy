using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Data.ValueObject;
using Data.UnityObject;
using Zenject;

public class EnemyAnimationController : EnemyAnimationControllerBase
{
    #region Self Variables
    [Inject] private EnemySettings _settings { get; set; }

    #region Serialized Variables

    [SerializeField] private Animator animator;
    #endregion
    #region Private Variables
    private UIData _uiData;

    #endregion
    #endregion

    public UIData GetData() => Resources.Load<CD_UI>("Data/CD_UI").Data;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _uiData = GetData();
    }

    public override void OnChangeAnimation(PlayerAnimationStates nextAnimation)
    {
        if (nextAnimation == PlayerAnimationStates.Reload)
        {
            animator.speed = _settings.ReloadSpeed;
        }
        else
        {
            animator.speed = 1;

        }
        animator.SetTrigger(nextAnimation.ToString());
    }
}