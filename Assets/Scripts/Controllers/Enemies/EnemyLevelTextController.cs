using Enums;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

class EnemyLevelTextController : MonoBehaviour
{
    [Inject] private LoadGameDataCommand _loadCommand { get; set; }
    [Inject] private EnemyModel _model { get; set; }
    [SerializeField] TextMeshPro _text; 

    private void Awake()
    {
        int episodeLevel = _model.Level;
        _text.text = "Lvl: " + (episodeLevel + 1).ToString();
    }
}