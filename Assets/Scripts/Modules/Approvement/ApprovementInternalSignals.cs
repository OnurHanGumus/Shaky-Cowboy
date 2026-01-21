using UnityEngine;
using UnityEngine.Events;
using Zenject;

class ApprovementInternalSignals
{
    public UnityAction<bool> onDecisionMade = delegate { };
}