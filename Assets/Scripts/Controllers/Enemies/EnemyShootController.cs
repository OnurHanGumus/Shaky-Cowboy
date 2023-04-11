using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System;
using Signals;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EnemyShootController : MonoBehaviour
    {
        #region Self Variables
        #region Inject Variables
        [Inject] private EnemySettings EnemySettings { get; set; }
        [Inject] private EnemySignals EnemySignals { get; set; }
        #endregion
        #region Public Variables
        #endregion
        #region Serializefield Variables
        [SerializeField] private Transform bulletHolder;
        [SerializeField] private List<Component> gunList; //We use "Component" reference to see interfaces in Unity Inspector. Or we can also use their common base class "GunController". 
        [SerializeField] private List<GameObject> gunMeshes;

        #endregion
        #region Private Variables
        private Settings _mySettings;
        private int _selectedGunId = 0;
        private IGun _currentGun;
        #endregion
        #endregion

        private void Awake()
        {
            _mySettings = EnemySettings.EnemyShootSettings;
            _currentGun = (IGun) gunList[0];
        }
        private void Start()
        {
            ShootDelay();
        }
        private void Shoot()
        {
            EnemySignals.onShoot?.Invoke();
        }

        private async Task ShootDelay()
        {
            while (true)
            {
                await Task.Delay((int)(Random.Range(0.15f, 0.85f) * 1000));
                Shoot();
            }
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 ShootOffset;
        }
    }
}