using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System;
using Data.MetaData;
using Components.Enemies;
using Signals;
using System.Threading.Tasks;

namespace Controllers
{
    public class PlayerShootController : PlayerShootControllerBase
    {
        #region Self Variables
        #region Inject Variables
        [Inject] private PlayerSettings PlayerSettings { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private AudioSignals AudioSignals { get; set; }
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

        public override void OnClicked()
        {
            if (_isDied)
            {
                return;
            }
            Shoot();
        }
        private void Shoot()
        {
            _currentGun.OnShoot();
            --_currentGun.CurrentBulletCount;
            if (_currentGun.CurrentBulletCount <= 0)
            {
                if (_isReloading)
                {
                    AudioSignals.onPlaySound?.Invoke(AudioSoundEnums.Reload);
                    return;
                }

                _currentGun.Reload();
            }
            AudioSignals.onPlaySound?.Invoke(AudioSoundEnums.Shoot);
        }

        private void ChangeGun()
        {
            _currentGun = (IGun)gunList[_selectedGunId];
            for (int i = 0; i < gunList.Count; i++)
            {
                gunMeshes[i].SetActive(false);
            }
            gunMeshes[_selectedGunId].SetActive(true);
        }

        public override void OnReload()
        {
            _isReloading = true;

        }

        public override void OnDie(StickmanBodyPartEnums bodyPart)
        {
            _isDied = true;
        }

        public override void OnRestart()
        {
            _isDied = false;
            _isReloading = false;
        }

        public override void OnReloaded()
        {
            _isReloading = false;
        }
    }
}