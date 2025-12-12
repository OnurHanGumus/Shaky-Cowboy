using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Data.ValueObject;
using Data.UnityObject;
using Zenject;

public class PlayerAnimationController : PlayerAnimationControllerBase
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Animator animator;
    [Inject] private PlayerSettings _settings { get; set; }

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
            animator.speed = _settings.Settings[UpgradeEnums.ReloadSpeed];
        }
        else
        {
            animator.speed = 1;

        }
        animator.SetTrigger(nextAnimation.ToString());
    }

    public override void OnRestartLevel()
    {
        animator.Rebind();
        animator.Update(0f);
    }
}