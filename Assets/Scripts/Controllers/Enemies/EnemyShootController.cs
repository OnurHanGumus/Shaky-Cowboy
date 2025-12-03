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
        [Inject] private AudioSignals AudioSignals { get; set; }
        [Inject] private EnemySettings _settings { get; set; }
        [Inject] private EnemyShootDelaySettings _shootDelaySettings { get; set; }
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
        private int _selectedGunId = 0;
        private IGun _currentGun;
        private bool _isDied = false;
        private bool _isReloading = false;

        #endregion
        #endregion

        private void Awake()
        {
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
            _currentGun.OnShoot();
            --_currentGun.CurrentBulletCount;
            if (_currentGun.CurrentBulletCount <= 0)
            {
                if (_isReloading)
                {
                    return;
                }
                _isReloading = true;
                AudioSignals.onPlaySound?.Invoke(AudioSoundEnums.Reload);
                _currentGun.Reload();
            }

            AudioSignals.onPlaySound?.Invoke(AudioSoundEnums.Shoot);
        }

        private async Task ShootDelay()
        {
            while (true)
            {
                await Task.Delay((int)(Random.Range(_shootDelaySettings.EnemyShootDelayList[(int)_settings.ShootDelayEnum].MinDelay, 
                    _shootDelaySettings.EnemyShootDelayList[(int)_settings.ShootDelayEnum].MaxDelay) * 1000));
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

        public override void OnReloaded()
        {
            _isReloading = false;
        }

        public override void OnDie(StickmanBodyPartEnums bodyPart)
        {
            _isDied = true;
        }
    }
}