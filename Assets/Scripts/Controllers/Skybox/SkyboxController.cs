using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SkyboxController : MonoBehaviour
{
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }
    [Inject] private LevelSignals _levelSignals { get; set; }
    [SerializeField] private List<Material> skyboxes;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        SubscribeEvents();
        ChangeSkybox();
    }

    private void SubscribeEvents()
    {
        _coreGameSignals.onNextLevel += ChangeSkybox;
    }

    private void ChangeSkybox()
    {
        int levelId = _levelSignals.onGetCurrentModdedLevel();
        RenderSettings.skybox = skyboxes[levelId%skyboxes.Count];
    }
}