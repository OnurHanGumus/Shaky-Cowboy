using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;

class UpgradeButtonController : IInitializable
{
    [Inject] protected CoreGameSignals _coreGameSignals { get; set; }
    [Inject] protected UpgradeButtonInternalSignals _internalSignals { get; set; }
    [Inject] protected UpgradeButtonModel _model { get; set; }
    [Inject] private LoadGameDataCommand _loadCommand { get; set; }
    [Inject] private SaveGameCommand _saveCommand { get; set; }
    [Inject] private UpgradeSettings _upgradeSettings { get; set; }

    public void Initialize()
    {
        SubscribeEvents();
        UpdateTexts();
    }

    private void SubscribeEvents()
    {
        _internalSignals.onButtonClicked += Upgrade;
    }

    private void Upgrade(int upgradeId) //button
    {
        //checks  if it fully upgraded
        //if not, checks if player has enough money
        //if he has, lowers money, increases its level and fires onUpgradePurchased signal
        int currentLevel =_loadCommand.OnLoadGameData<int>(_model.SaveDataEnum);
        if (currentLevel >= _upgradeSettings.Skills[_model.UpgradeEnum].Count)
        {
            return;
        }
        _saveCommand.OnSaveData<int>(_model.SaveDataEnum, ++currentLevel);
        _coreGameSignals.onUpgradePurchased?.Invoke((UpgradeEnums)upgradeId, currentLevel);


        UpdateTexts();
    }

    private void UpdateTexts()
    {
        int currentLevel = _loadCommand.OnLoadGameData<int>(_model.SaveDataEnum);
        int abilityMaxLevel = _upgradeSettings.Skills[_model.UpgradeEnum].Count;
        _model.LevelText.text = currentLevel.ToString();

        if (currentLevel < abilityMaxLevel)
        {
            _model.PriceText.text = _upgradeSettings.Skills[_model.UpgradeEnum][currentLevel].UpgradePrices.ToString();
        }
        else
        {
            _model.PriceText.text = "MAX";
        }
    }
}