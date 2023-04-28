using Enums;

namespace Controllers
{
    public interface IPlayerShootController
    {
        void OnClicked();
        void OnDie(StickmanBodyPartEnums bodyPart);
        void OnReload();
        void OnRestart();
    }
}