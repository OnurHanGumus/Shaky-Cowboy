using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zenject;
using UnityEngine;
using System.Collections;

class MusicController : MonoBehaviour
{
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }

    private AudioSource _audioSource { get; set; }
    private WaitForSeconds _seconds = new WaitForSeconds(0.1f);

    private void Awake()
    {
        Init();
        SubscribeEvents();
    }

    private void Init()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void SubscribeEvents()
    {
        _coreGameSignals.onPlay += OnPlay;
        _coreGameSignals.onRestart += OnRestart;
    }

    private void OnRestart()
    {
        StopAllCoroutines();
        StartCoroutine(MusicVolumeIncreaser());
    }

    private void OnPlay()
    {
        StopAllCoroutines();
        StartCoroutine(MusicVolumeDecreaser());
    }

    private IEnumerator MusicVolumeIncreaser()
    {
        while (_audioSource.volume < 0.5f)
        {
            yield return _seconds;
            _audioSource.volume += 0.1f;
        }
    }

    private IEnumerator MusicVolumeDecreaser()
    {
        while (_audioSource.volume > 0.3f)
        {
            yield return _seconds;
            _audioSource.volume -= 0.1f;
        }
    }
}