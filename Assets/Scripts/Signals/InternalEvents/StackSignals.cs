using Enums;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals
    {
        public Func<int> onGetStackRemainPlace = delegate { return 0; };
        public UnityAction<Transform> onMoneyWorkerCollectMoney = delegate { };
        public UnityAction<int> onStackIncreased = delegate { };
        public UnityAction<int> onStackDecreased = delegate { };


    }
}