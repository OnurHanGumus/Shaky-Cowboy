using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "HapticUiSettings", menuName = "Data/HapticUiSettings", order = 0)]
public class HapticUiSettings : ScriptableObject
{
    [SerializedDictionary("Name", "Value")]
    public SerializedDictionary<HapticUiEnums, HapticUi> HapticDictionary;
}