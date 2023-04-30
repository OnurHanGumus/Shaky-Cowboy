using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class RevolverData
    {
        public Vector3 RevolverHandLocalPosition = new Vector3(-0.357f, -0.361f, 0.219f),
            RevolverEulerLocalAngle = new Vector3(-55.102f, 130.775f, -211.855f);
    }
}