using Components.Players;
using UnityEngine;

namespace Data.MetaData
{
    [CreateAssetMenu(fileName = "TumbleweedSpawnSettings", menuName = "ZenjectExample/TumbleweedSpawnSettings", order = 0)]
    public class TumbleweedSpawnSettings : ScriptableObject
    {
        [SerializeField] public TumbleweedSpawnManager.Settings SpawnSettings;

    }
}