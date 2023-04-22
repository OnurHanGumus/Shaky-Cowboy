using Components.Players;
using UnityEngine;

namespace Signals
{
    [CreateAssetMenu(fileName = "TumbleweedSettings", menuName = "ZenjectExample/TumbleweedSettings", order = 0)]
    public class TumbleweedSettings : ScriptableObject
    {
        [SerializeField] public TumbleweedManager.Settings Settings;
    }
}