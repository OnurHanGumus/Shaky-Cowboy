using System;
using Controllers;
using Data.MetaData;
using UnityEngine;
using Zenject;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "MySettings/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    public SerializedDictionary<UpgradeEnums, float> Settings;
}