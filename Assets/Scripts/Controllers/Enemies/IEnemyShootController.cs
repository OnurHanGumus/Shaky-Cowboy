using Enums;

namespace Controllers
{
    public interface IEnemyShootController
    {
        void OnDie(StickmanBodyPartEnums bodyPart);
        void OnPlay();
        void OnReload();
    }
}