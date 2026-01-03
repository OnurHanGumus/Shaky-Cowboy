using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using Controllers;
using Managers;
using Enums;
using Signals;
using GoogleMobileAds.Api;
using Zenject;

public class AdInterstitialManager : MonoBehaviour
{
    [SerializeField] protected bool isTest = true;

    protected string _testInterstitialId = "ca-app-pub-3940256099942544/1033173712",
                       _interstitialId = "ca-app-pub-4708991160900195/3885741590";
    protected InterstitialAd _interstitialAd = null;
    protected AdRequest _adRequest = null;
    protected bool _isFirstTime = true;
    [Inject] protected CoreGameSignals _coreGameSignals { get; set; }

    protected void Awake()
    {
        Init();
        RequestAd();
    }

    #region Event Subscription
    protected void OnEnable()
    {
        SubscribeEvents();
    }

    protected void SubscribeEvents()
    {
        _coreGameSignals.onPlay += OnPlay;
    }

    protected void UnsubscribeEvents()
    {
        _coreGameSignals.onPlay -= OnPlay;
    }

    protected void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    protected void OnPlay()
    {
        if (!_isFirstTime)
        {
            RequestAd();
        }
        _isFirstTime = false;
    }

    protected void Init()
    {
        MobileAds.Initialize((InitializationStatus initStatus) => { });
    }

    protected void RequestAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        _adRequest = new AdRequest();
        _adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(isTest ? _testInterstitialId : _interstitialId, _adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              if (error != null || ad == null)
              {
                  return;
              }
              _interstitialAd = ad;
          });
    }

    public virtual void ShowAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
        }
    }

    #region AdEvents Not Working -- Use normal signals instead of this
    /// <summary>
    /// listen to events the banner may raise.
    /// </summary>
    protected void ListenToAdEvents(InterstitialAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            _isFirstTime = true;
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            _isFirstTime = true;
        };
    }
    #endregion
}