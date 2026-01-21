using Zenject;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

class ApproveModel
{
    public TextMeshProUGUI ApproveText { get; set; }
    [Inject] public ApprovementPanelSettings PanelSettings { get; set; }
    public CanvasGroup CanvasGroup { get; set; }
    public List<TextMeshProUGUI> Texts { get; set; }

}