using Components.Players;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSettings", menuName = "MySettings/UpgradeSettings", order = 0)]
public class UpgradeSettings : ScriptableObject
{
    [SerializeField] public List<float> UpgradeValues;
    [SerializeField] public List<int> UpgradePrices;
}