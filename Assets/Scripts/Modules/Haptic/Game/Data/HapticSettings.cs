using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "HapticSettings", menuName = "Data/HapticSettings", order = 0)]
public class HapticSettings : ScriptableObject
{
    [SerializedDictionary("Name", "Value")]
    public SerializedDictionary<HapticEnums, Haptic> HapticDictionary;
}