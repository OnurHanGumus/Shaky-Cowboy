using Components.Enemies;
using UnityEngine.Events;

namespace Signals
{
    public class EnemySignals
    {
        public UnityAction<IAttackable> onDeath = delegate { };
        public UnityAction<int> onHitted = delegate { };
        public UnityAction onShoot = delegate { };
        public UnityAction onReload = delegate { };
        public UnityAction onReloaded = delegate { };
    }
}