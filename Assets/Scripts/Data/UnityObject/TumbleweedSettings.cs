using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Tumbleweed", menuName = "Picker3D/CD_Tumbleweed", order = 0)]
    public class TumbleweedSettings : ScriptableObject
    {
        public TumbleweedData Data;
    }
}