using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

class UpgradeButtonView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] TextMeshProUGUI ValueText;
    [SerializeField] TextMeshProUGUI DescriptionText;
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] Image Image;
    [SerializeField] UpgradeEnums UpgradeEnum;
    [SerializeField] SaveDataEnums SaveDataEnum;
    [Inject] protected UpgradeButtonInternalSignals _internalSignals { get; set; }

    [Inject]
    private void Constructor(UpgradeButtonModel model)
    {
        model.LevelText = LevelText;
        model.ValueText = ValueText;
        model.DescriptionText = DescriptionText;
        model.PriceText = PriceText;
        model.Image = Image;
        model.UpgradeEnum = UpgradeEnum;
        model.SaveDataEnum = SaveDataEnum;
    }

    public void OnButtonClick(int id)
    {
        _internalSignals.onButtonClicked?.Invoke(id);
    }
}