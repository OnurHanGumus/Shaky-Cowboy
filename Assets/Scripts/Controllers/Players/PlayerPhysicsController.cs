using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Enums;
using Signals;
using Components.Enemies;

namespace Controllers {
    public class PlayerPhysicsController : MonoBehaviour, IAttackable
    {
        #region Self Variables
        #region Inject Variables
        [Inject] private LevelSignals LevelSignals { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        #endregion
        #region Public Variables
        #endregion
        #region Serializefield Variables
        [SerializeField] private int criticalDamageValue = 5;
        [SerializeField] private StickmanBodyPartEnums stickmanBodyPartEnums;
        #endregion
        #region Private Variables
        #endregion
        #region Properties
        #endregion
        #endregion

        private void OnDisable()
        {
        }

        void IAttackable.OnWeaponTriggerEnter(float value)
        {
            PlayerSignals.onHitted?.Invoke(value * criticalDamageValue, stickmanBodyPartEnums);
        }
    }
}