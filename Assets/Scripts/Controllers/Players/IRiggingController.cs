using Enums;

namespace Controllers
{
    public interface IPlayerRiggingController
    {
        void OnDie(StickmanBodyPartEnums bodyPart);
        void OnPlay();
        void OnReload();
        void OnReloaded();
    }
}