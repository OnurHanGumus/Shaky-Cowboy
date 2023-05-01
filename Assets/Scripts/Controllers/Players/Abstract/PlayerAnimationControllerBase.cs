using Enums;
using UnityEngine;

public abstract class PlayerAnimationControllerBase : MonoBehaviour
{
    public abstract void OnChangeAnimation(PlayerAnimationStates nextAnimation);
    public abstract void OnRestartLevel();
}