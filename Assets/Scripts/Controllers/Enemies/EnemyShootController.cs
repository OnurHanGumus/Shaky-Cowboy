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
    public class EnemyShootController : EnemyShootControllerBase
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
        [SerializeField] private Transform playerLeftHand;
        [SerializeField] private Transform revolver;
        [SerializeField] private List<Component> gunList; //We use "Component" reference to see interfaces in Unity Inspector. Or we can also use their common base class "GunController". 
        [SerializeField] private List<GameObject> gunMeshes;

        #endregion
        #region Private Variables
        private Settings _mySettings;
        private int _selectedGunId = 0;
        private IGun _currentGun;
        private bool _isDied = false;

        #endregion
        #endregion

        private void Awake()
        {
            _mySettings = EnemySettings.EnemyShootSettings;
            _currentGun = (IGun)gunList[0];
        }

        private void Start()
        {

        }

        public override void OnPlay()
        {
            ShootDelay();
        }

        private void Shoot()
        {
            //EnemySignals.onShoot?.Invoke();
            _currentGun.OnShoot();
            --_currentGun.CurrentBulletCount;
            if (_currentGun.CurrentBulletCount <= 0)
            {
                _currentGun.Reload();
                return;
            }
        }

        private async Task ShootDelay()
        {
            while (true)
            {
                await Task.Delay((int)(Random.Range(0.5f, 1f) * 1000));
                if (_isDied)
                {
                    break;
                }
                Shoot();
            }
        }

        public override void OnReload()
        {

        }

        public override void OnDie(StickmanBodyPartEnums bodyPart)
        {
            _isDied = true;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 ShootOffset;
        }
    }
}