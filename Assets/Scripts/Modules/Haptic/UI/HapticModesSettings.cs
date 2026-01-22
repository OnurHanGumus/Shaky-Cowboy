using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "HapticModesSettings", menuName = "Data/HapticModesSettings", order = 0)]
public class HapticModesSettings : ScriptableObject
{
    [SerializedDictionary("Name", "Value")]
    public SerializedDictionary<HapticUiEnums, List<HapticEnums>> AllowedHaptics;
}