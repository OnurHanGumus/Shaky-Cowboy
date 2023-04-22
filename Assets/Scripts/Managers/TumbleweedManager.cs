using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Signals;
using System;

public class TumbleweedManager : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    #endregion
    #region Protected Variables

    #endregion
    #region Private Variables
    private Settings _mySettings;

    #endregion
    #region Properties

    #endregion
    #endregion
    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
        StartCoroutine(BulletLifeTime());
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onRestart += OnRestartLevel;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onRestart -= OnRestartLevel;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion
    [Inject]
    public void Constractor(TumbleweedSettings tumbleweedSettings)
    {
        _mySettings = tumbleweedSettings.Settings;
    }

    private void OnRestartLevel()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(_mySettings.LifeTime);
        DespawnSignal();
    }

    private void DespawnSignal()
    {
        gameObject.SetActive(false);
    }

    public class Factory : PlaceholderFactory<TumbleweedManager>, IPool
    {
        GameObject IPool.OnCreate()
        {
            return base.Create().gameObject;
        }
    }

    [Serializable]
    public class Settings
    {
        [SerializeField] public float LifeTime = 5f;
    }
}
