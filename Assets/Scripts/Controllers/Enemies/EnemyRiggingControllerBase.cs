using Enums;
using UnityEngine;

namespace Controllers
{
    public abstract class EnemyRiggingControllerBase : MonoBehaviour
    {
        public abstract void OnDie(StickmanBodyPartEnums bodyPart);
        public abstract void OnPlay();
        public abstract void OnReload();
        public abstract void OnReloaded();
    }
}