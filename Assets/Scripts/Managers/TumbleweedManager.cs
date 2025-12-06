using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Signals;
using System;
using Data.ValueObject;
using Data.UnityObject;

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
    //private Settings _mySettings;
    private TumbleweedData _data;
    #endregion
    #region Properties

    #endregion
    #endregion
    private TumbleweedData GetData() => Resources.Load<TumbleweedSettings>("Data/TumbleweedSettings").Data;

    private void Awake()
    {
        _data = GetData();
    }

    #region Event Subscription
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

    private void OnRestartLevel()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(_data.LifeTime);
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
}
