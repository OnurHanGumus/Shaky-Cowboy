using Enums;

namespace Controllers
{
    public interface IEnemyRiggingController
    {
        void OnDie(StickmanBodyPartEnums bodyPart);
        void OnPlay();
        void OnReload();
        void OnReloaded();
    }
}