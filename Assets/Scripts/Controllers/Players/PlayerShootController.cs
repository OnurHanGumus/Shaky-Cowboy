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
    public class PlayerShootController : MonoBehaviour
    {
        #region Self Variables
        #region Inject Variables
        [Inject] private PlayerSettings PlayerSettings { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        #endregion
        #region Public Variables
        #endregion
        #region Serializefield Variables
        [SerializeField] Vector3 playerCurrentPos;
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
            _mySettings = PlayerSettings.PlayerShootManagerSettings;
        }

        public void OnClicked()
        {
            Shoot();
        }
        private void Shoot()
        {
            PlayerSignals.onShoot?.Invoke();
        }
        private void Reload()
        {

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

        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 ShootOffset;
        }
    }
}