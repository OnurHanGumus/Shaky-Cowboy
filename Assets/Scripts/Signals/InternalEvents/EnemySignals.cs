using Components.Enemies;
using UnityEngine.Events;

namespace Signals
{
    public class EnemySignals
    {
        public UnityAction<IAttackable> onDeath = delegate { };
        public UnityAction onShoot = delegate { };
    }
}