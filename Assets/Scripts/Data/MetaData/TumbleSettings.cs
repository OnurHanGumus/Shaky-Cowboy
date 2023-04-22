using Components.Players;
using UnityEngine;

namespace Data.MetaData
{
    [CreateAssetMenu(fileName = "TumbleSettings", menuName = "ZenjectExample/TumbleSettings", order = 0)]
    public class TumbleSettings : ScriptableObject
    {
        [SerializeField] public TumbleweedManager.Settings TumbleweedManagerSettings;

    }
}