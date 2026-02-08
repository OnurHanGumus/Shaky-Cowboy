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
    [Inject] private ScoreSignals _scoreSignals { get; set; }
    [Inject] private IApprovement _approvement { get; set; }
    [Inject] private PlayerSettings _playerSettings { get; set; }
    [Inject] private AudioSignals _audioSignals { get; set; }

    private int _upgradeIdTemp = 0;
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
        _upgradeIdTemp = upgradeId;
        int currentLevel =_loadCommand.OnLoadGameData<int>(_model.SaveDataEnum);
        if (currentLevel >= _upgradeSettings.Skills[_model.UpgradeEnum].Count)
        {
            return;
        }
        int currentMoney = _scoreSignals.onGetMoney();
        int price = _upgradeSettings.Skills[_model.UpgradeEnum][currentLevel].UpgradePrices;
        if (currentMoney < price)
        {
            return;
        }
        else
        {
            _approvement.SetApprovementCondition(UpgradeProcess);
        }

    }

    private void UpgradeProcess()
    {
        int currentLevel = _loadCommand.OnLoadGameData<int>(_model.SaveDataEnum);
        int price = _upgradeSettings.Skills[_model.UpgradeEnum][currentLevel].UpgradePrices;
        _scoreSignals.onAmountChanged?.Invoke(-price);
        _saveCommand.OnSaveData<int>(_model.SaveDataEnum, ++currentLevel);
        _coreGameSignals.onUpgradePurchased?.Invoke((UpgradeEnums)_upgradeIdTemp, currentLevel);
        _audioSignals.onPlaySound?.Invoke(Enums.AudioSoundEnums.Buy);

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
            if (!_loadCommand.CheckIfKeyInitialized(_model.SaveDataEnum))
            {
                _model.DescriptionText.text = _playerSettings.Settings[_model.UpgradeEnum] + " > " + _upgradeSettings.Skills[_model.UpgradeEnum][currentLevel].UpgradeValue.ToString();

            }
            else
            {
                _model.DescriptionText.text = _upgradeSettings.Skills[_model.UpgradeEnum][currentLevel-1].UpgradeValue.ToString() + " > " + _upgradeSettings.Skills[_model.UpgradeEnum][currentLevel].UpgradeValue.ToString();
            }

        }
        else
        {
            _model.PriceText.text = "MAX";
            _model.DescriptionText.text = _upgradeSettings.Skills[_model.UpgradeEnum][currentLevel-1].UpgradeValue.ToString();
        }

    }
}