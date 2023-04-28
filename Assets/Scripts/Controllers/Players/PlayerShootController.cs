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
            _mySettings = PlayerSettings.PlayerShootManagerSettings;
            _currentGun = (IGun) gunList[0];
        }

        public void OnClicked()
        {
            if (_isDied)
            {
                return;
            }
            Shoot();
        }
        private void Shoot()
        {
            //PlayerSignals.onShoot?.Invoke();
            _currentGun.OnShoot();
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
        private void RevolverOnHand()
        {
            revolver.parent = playerLeftHand;
            revolver.localPosition = new Vector3(-0.357f, -0.361f, 0.219f);
            revolver.localEulerAngles = new Vector3(-55.102f, 130.775f, -211.855f);
        }

        public void OnReload()
        {
            RevolverOnHand();
        }

        public void OnDie(StickmanBodyPartEnums bodyPart)
        {
            _isDied = true;
            RevolverOnHand();
        }

        public void OnRestart()
        {
            _isDied = false;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 ShootOffset;
        }
    }
}