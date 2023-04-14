using Components.Enemies;
using UnityEngine.Events;
using Enums;


namespace Signals
{
    public class EnemySignals
    {
        public UnityAction<StickmanBodyPartEnums> onDie = delegate { };
        public UnityAction<int, StickmanBodyPartEnums> onHitted = delegate { };
        public UnityAction onShoot = delegate { };
        public UnityAction onReload = delegate { };
        public UnityAction onReloaded = delegate { };
    }
}