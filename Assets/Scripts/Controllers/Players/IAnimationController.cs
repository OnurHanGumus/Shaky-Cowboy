using Data.ValueObject;
using Enums;

public interface IPlayerAnimationController
{
    UIData GetData();
    void OnChangeAnimation(PlayerAnimationStates nextAnimation);
    void OnRestartLevel();
}