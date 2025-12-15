using System;
using Enums;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {

    }

    private void UnsubscribeEvents()
    {

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}