using System;
using Data.MetaData;
using Signals;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _myTransform;
        
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private void Awake()
        {
        }

        private void OnEnable()
        {
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private void RegisterEvents()
        {
            //MainSceneEvents.GameLoaded += OnGameLoaded;
        }

        //private void OnGameLoaded(){
        //loadPlayer
        //}

        private void UnRegisterEvents()
        {
        }
    }
}
