using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

class BulletTimeController : MonoBehaviour
{
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }

    [SerializeField] private float bulletTimeTimeScale = 0.5f;
    [SerializeField] private bool isBulletTimeActivated = false;
    [SerializeField] private float delay = 0.05f;
    [SerializeField] private float increaseAmountOfEveryDelay = 0.05f;

    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _waitForSeconds = new WaitForSeconds(delay);
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _coreGameSignals.onBulletTimeActivated += OnBulletTimeActivated;
        _coreGameSignals.onLevelFailed += OnReset;
        _coreGameSignals.onLevelSuccessful += OnReset;
        _coreGameSignals.isBulletTimeActivated += IsBulletTimeActivated;
    }

    private bool IsBulletTimeActivated()
    {
        return isBulletTimeActivated;
    }

    private void OnReset()
    {
        StopAllCoroutines();

        StartCoroutine(ExitAnimation());
        isBulletTimeActivated = false;
    }

    private void OnBulletTimeActivated()
    {
        StopAllCoroutines();
        if (isBulletTimeActivated)
        {
            StartCoroutine(ExitAnimation());
            isBulletTimeActivated = false;

        }
        else
        {
            StartCoroutine(EnterAnimation());
            isBulletTimeActivated = true;
        }
    }

    IEnumerator EnterAnimation()
    {
        while (Time.timeScale > bulletTimeTimeScale)
        {
            yield return _waitForSeconds;
            Time.timeScale -= increaseAmountOfEveryDelay;
        }
        _coreGameSignals.onBulletTimeAnimationCompleted?.Invoke();
    }

    IEnumerator ExitAnimation()
    {
        while (Time.timeScale < 1f)
        {
            yield return _waitForSeconds;
            Time.timeScale += increaseAmountOfEveryDelay;
        }
        _coreGameSignals.onBulletTimeAnimationCompleted?.Invoke();
    }
}