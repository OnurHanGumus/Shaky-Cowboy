using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using Enums;
using Zenject;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables
        #region Inject Variables
        [Inject] private InputSignals InputSignals { get; set; }
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        #endregion
        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables

        //[SerializeField] FloatingJoystick joystick; //SimpleJoystick paketi eklenmeli


        #endregion

        #region Private Variables

        private Ray _ray;
        #endregion

        #endregion


        private void Awake()
        {
            Data = GetInputData();
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").Data;


        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            if (IsPointerOverUIElement())
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                InputSignals.onClicked?.Invoke(/*_clickedTransform*/);
            }

            if (Input.GetMouseButtonUp(0))
            {
                InputSignals.onInputReleased?.Invoke();
            }

        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }

        private void OnReset()
        {

        }
    }
}