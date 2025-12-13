using Components.Players;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "UpgradeSettings", menuName = "MySettings/UpgradeSettings", order = 0)]
public class UpgradeSettings : ScriptableObject
{
    public SerializedDictionary<UpgradeEnums, List<Skill>> Skills;
}

[System.Serializable]
public struct Skill
{
    [SerializeField] public float UpgradeValue;
    [SerializeField] public int UpgradePrices;
}