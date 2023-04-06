using Data.MetaData;
using Events.External;
using Signals;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [Inject] private InputSignals InputSignals { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private RoutineHelper _onPosUpdate;
        private bool _isStarted = false;
        private Settings _mySettings;
        
        private void Awake()
        {
            _mySettings = PlayerSettings.PlayerMovementSettings;
        }

        #region Event Subscription
        private void OnEnable()
        {
            RegisterEvents();
        }
        

        private void RegisterEvents()
        {

        }

       

        private void UnRegisterEvents()
        {

        }
        private void OnDisable()
        {
            UnRegisterEvents();
        }
        #endregion

        public void OnPlay()
        {
            _isStarted = true;
        }
        public void OnRestartLevel()
        {
            _isStarted = false;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
        }
    }
}