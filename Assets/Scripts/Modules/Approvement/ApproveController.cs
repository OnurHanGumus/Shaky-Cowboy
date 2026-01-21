using Zenject;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

class ApproveController : Zenject.IInitializable, IDisposable ,IApprovement
{
    [Inject] private ApproveModel _model { get; set; }
    [Inject] private ApprovementInternalSignals _internalSignals { get; set; }
    //[Inject] private ButtonClickEffectsCommand _clickCommand { get; set; }
    //[Inject] private TranslateCommand _translateCommand { get; set; }
    //[Inject] private LanguageSignals _languageSignals { get; set; }
    //[Inject(Id ="Approvement")] private PanelTextSettings _panelTextSettings { get; set; }

    private UnityAction _approveAction;
    [Inject]
    private void Constructor()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _internalSignals.onDecisionMade += OnDecisionMade;
        //_languageSignals.onChange += OnChange;
    }

    //private void OnChange(LanguageEnums arg0)
    //{
    //    _translateCommand.Translate(_model.Texts, _panelTextSettings);
    //}

    public void SetApprovementCondition(UnityAction action)
    {
        ChangePanelActiveness(isOpen: true);
        _approveAction = action;
    }

    public void OnDecisionMade(bool selection)
    {
        if (selection==true)
        {
            _approveAction.Invoke();
        }

        ChangePanelActiveness(isOpen:false);
        //_clickCommand.PlayClickEffects(ButtonClickTypeEnums.Both);
    }

    private void ChangePanelActiveness(bool isOpen)
    {
        _model.CanvasGroup.blocksRaycasts = isOpen;
        _model.CanvasGroup.alpha = isOpen ? 1:0;
    }

    private void ChangeDescription(string descText)
    {
        _model.ApproveText.text = descText;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        //_languageSignals.onChange -= OnChange;
    }
}