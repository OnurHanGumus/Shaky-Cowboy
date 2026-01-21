using Zenject;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ApproveView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI approvementText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] List<TextMeshProUGUI> texts;

    [Inject] private ApprovementInternalSignals _internalSignals { get; set; }

    [Inject]
    private void Contructor(ApproveModel model)
    {
        model.ApproveText = approvementText;
        model.CanvasGroup = canvasGroup;
        model.Texts = texts;
    }

    public void DecisionButton(bool decision)
    {
        _internalSignals.onDecisionMade?.Invoke(decision);
    }
}
