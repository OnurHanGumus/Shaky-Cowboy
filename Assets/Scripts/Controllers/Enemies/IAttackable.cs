using Signals;
using UnityEngine.Events;

namespace Components.Enemies
{
    public interface IAttackable
    {
        void OnWeaponTriggerEnter();
        EnemySignals GetInternalEvents();
    }
}