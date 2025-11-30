using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Enums;
using Signals;
using Components.Enemies;
using System;

namespace Controllers {
    public class EnemyPhysicsController : MonoBehaviour, IAttackable
    {
        #region Self Variables
        #region Inject Variables
        [Inject] private EnemySignals EnemySignals { get; set; }
        #endregion
        #region Public Variables
        #endregion
        #region Serializefield Variables
        [SerializeField] private EnemyManager enemy;
        [SerializeField] private int criticalDamageValue = 5;
        [SerializeField] private StickmanBodyPartEnums stickmanBodyPartEnums;
        #endregion
        #region Private Variables
        #endregion
        #region Properties
        #endregion
        #endregion

        void IAttackable.OnWeaponTriggerEnter(int value)
        {
            EnemySignals.onHitted?.Invoke(value * criticalDamageValue, stickmanBodyPartEnums);
        }
    }
}