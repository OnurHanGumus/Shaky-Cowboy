using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public float Speed = 5, AngularSpeed = 10;
        public int InitializePosX, InitializePosY;
    }
}