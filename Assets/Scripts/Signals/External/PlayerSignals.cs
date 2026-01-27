using Components.Enemies;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using Enums;

namespace Signals
{
    [UsedImplicitly]
    public class PlayerSignals
    {
        public UnityAction<Vector3> onPlayerMove;
        public UnityAction<IAttackable> onEnemyShooted;
        public UnityAction onShoot;
        public UnityAction<float, StickmanBodyPartEnums> onHitted = delegate { };
        public UnityAction<StickmanBodyPartEnums> onDie = delegate { };
        public UnityAction onReload;
        public UnityAction onReloaded;

    }
}