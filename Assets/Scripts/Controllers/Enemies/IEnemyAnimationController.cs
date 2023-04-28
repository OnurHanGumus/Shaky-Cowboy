using Data.ValueObject;
using Enums;

public interface IEnemyAnimationController
{
    UIData GetData();
    void OnChangeAnimation(PlayerAnimationStates nextAnimation);
}