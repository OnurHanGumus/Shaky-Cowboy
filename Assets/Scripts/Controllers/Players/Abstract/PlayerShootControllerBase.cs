using Enums;
using UnityEngine;

namespace Controllers
{
    public abstract class PlayerShootControllerBase : MonoBehaviour
    {
        public abstract void OnClicked();
        public abstract void OnDie(StickmanBodyPartEnums bodyPart);
        public abstract void OnReload();
        public abstract void OnRestart();
    }
}