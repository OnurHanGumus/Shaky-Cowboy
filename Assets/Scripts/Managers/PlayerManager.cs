using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables
        #region Injected Variables
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private InputSignals InputSignals { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        #endregion

        #region Public Variables

        #endregion

        #region Serialized Variables
        [SerializeField] private PlayerShootControllerBase shootController;
        [SerializeField] private PlayerAnimationControllerBase animationController;
        [SerializeField] private PlayerRiggingControllerBase riggingController;

        #endregion

        #region Private Variables
        private PlayerData _data;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
        }

        public PlayerData GetData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.onPlay += riggingController.OnPlay;
            CoreGameSignals.onRestart += animationController.OnRestartLevel;
            CoreGameSignals.onRestart += shootController.OnRestart;

            InputSignals.onClicked += shootController.OnClicked;

            PlayerSignals.onReload += OnReload;
            PlayerSignals.onReload += shootController.OnReload;
            PlayerSignals.onReload += riggingController.OnReload;
            PlayerSignals.onDie += OnDie;
            PlayerSignals.onDie += riggingController.OnDie;
            PlayerSignals.onDie += shootController.OnDie;


            PlayerSignals.onReloaded += riggingController.OnReloaded;
            PlayerSignals.onReloaded += shootController.OnReloaded;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.onPlay -= riggingController.OnPlay;
            CoreGameSignals.onRestart -= animationController.OnRestartLevel;
            CoreGameSignals.onRestart -= shootController.OnRestart;

            InputSignals.onClicked -= shootController.OnClicked;

            PlayerSignals.onReload -= OnReload;
            PlayerSignals.onReload -= shootController.OnReload;
            PlayerSignals.onReload -= riggingController.OnReload;
            PlayerSignals.onDie -= OnDie;
            PlayerSignals.onDie -= riggingController.OnDie;
            PlayerSignals.onDie -= shootController.OnDie;

            PlayerSignals.onReloaded -= riggingController.OnReloaded;
            PlayerSignals.onReloaded -= shootController.OnReloaded;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnDie(StickmanBodyPartEnums bodyPart)
        {
            animationController.OnChangeAnimation((PlayerAnimationStates)((int) bodyPart));
        }
        private void OnReload()
        {
            animationController.OnChangeAnimation(PlayerAnimationStates.Reload);
        }
    }
}