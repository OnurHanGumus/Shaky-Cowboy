using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Revolver", menuName = "Picker3D/CD_Revolver", order = 0)]
    public class CD_Revolver : ScriptableObject
    {
        public RevolverData Data;
    }
}