using Enums;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals
    {
        public Func<int> onGetCurrentModdedLevel = delegate { return 0; };
        public Func<int> onGetLevelId = delegate { return 0; };
        public Func<Transform> onGetLevelHolder = delegate { return null; };
        public UnityAction<Transform> onEnemyDied = delegate { };
        public UnityAction onEnemyArrived = delegate { };
        public UnityAction onStageComplated = delegate { };
        public UnityAction<Transform> onLastEnemyDied = delegate { };
        public UnityAction<int> onPreviousLevelOpened = delegate { };

    }
}