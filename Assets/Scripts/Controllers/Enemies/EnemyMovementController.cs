using Data.MetaData;
using Signals;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Components.Players
{
    public class EnemyMovementController : MonoBehaviour
    {

        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private EnemySettings EnemySettings { get; set; }

   
        private void Awake()
        {
        }
    }
}