using Cinemachine;
using Enums;
using Signals;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables
        #region Inject Variables
        [Inject] CoreGameSignals CoreGameSignals { get; set; }
        [Inject] LevelSignals LevelSignals { get; set; }
        #endregion
        #region Public Variables

        public CameraStatesEnum CameraStateController
        {
            get => _cameraStateValue;
            set
            {
                _cameraStateValue = value;
                SetCameraStates();
            }
        }

        #endregion
        #region Serialized Variables
        [SerializeField] private CinemachineVirtualCamera deadCam;
        #endregion

        #region Private Variables

        private Vector3 _initialPosition;
        private CameraStatesEnum _cameraStateValue = CameraStatesEnum.Initial;
        private Animator _camAnimator;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _camAnimator = GetComponent<Animator>();
        }

        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.onPlay += OnPlay;
            CoreGameSignals.onRestart += OnReset;

            LevelSignals.onLastEnemyDied += OnLastEnemyDie;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.onPlay -= OnPlay;
            CoreGameSignals.onRestart -= OnReset;

            LevelSignals.onLastEnemyDied -= OnLastEnemyDie;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SetCameraStates()
        {
            if (CameraStateController == CameraStatesEnum.Initial)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
            else if (CameraStateController == CameraStatesEnum.Play)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
            else if (CameraStateController == CameraStatesEnum.Dead)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
        }

        private void OnPlay()
        {
            CameraStateController = CameraStatesEnum.Play;
        }

        private void OnLastEnemyDie(Transform diedEnemy)
        {
            deadCam.Follow = diedEnemy;
            CameraStateController = CameraStatesEnum.Dead;
        }

        private void OnReset()
        {
            CameraStateController = CameraStatesEnum.Initial;
            Debug.Log("Initialize");
        }
    }
}