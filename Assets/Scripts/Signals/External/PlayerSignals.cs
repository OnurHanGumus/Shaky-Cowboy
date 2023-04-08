using Components.Enemies;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    [UsedImplicitly]
    public class PlayerSignals
    {
        public UnityAction<Vector3> onPlayerMove;
        public UnityAction<IAttackable> onEnemyShooted;
        public UnityAction onShoot;

    }
}