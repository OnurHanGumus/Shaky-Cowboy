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

public class AdBannerManager : MonoBehaviour
{
    [SerializeField] private bool isTest = true;

    private string _testBannerId = "ca-app-pub-3940256099942544/6300978111",
                       _bannerId = "ca-app-pub-4708991160900195/4745164174";
    private BannerView _bannerView = null;

    private void Awake()
    {
        Init();
        CreateBannerView();
        RequestBanner();
        ListenToAdEvents();
    }

    private void Init()
    {
        MobileAds.Initialize((InitializationStatus initStatus) => { });
    }

    public void CreateBannerView()
    {
        if (_bannerView != null)
        {
            DestroyAd();
        }
        AdSize adSize = new AdSize(320, 100);
        _bannerView = new BannerView(isTest ? _testBannerId : _bannerId, adSize, AdPosition.Bottom);
    }

    private void RequestBanner()
    {
        var adRequest = new AdRequest();
        _bannerView.LoadAd(adRequest);
    }

    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    #region AdEvents
    /// <summary>
    /// listen to events the banner may raise.
    /// </summary>
    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            RequestBanner();
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }
    #endregion
}