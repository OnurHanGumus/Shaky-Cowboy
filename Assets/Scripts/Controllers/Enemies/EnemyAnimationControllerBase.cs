using Enums;
using UnityEngine;

public abstract class EnemyAnimationControllerBase : MonoBehaviour
{
    public abstract void OnChangeAnimation(PlayerAnimationStates nextAnimation);
}