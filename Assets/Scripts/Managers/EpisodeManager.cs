using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using Signals;
using System;

public class EpisodeManager : MonoBehaviour
{
    [Inject] CoreGameSignals CoreGameSignals { get; set; }
    #region Event Subscriptions
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onPlay += OnPlay;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onPlay -= OnPlay;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion
    private void OnPlay()
    {

    }
    public class Factory : PlaceholderFactory<EpisodeManager>, IPool 
    {
        GameObject IPool.OnCreate()
        {
            return base.Create().gameObject;
        }
    }
}
