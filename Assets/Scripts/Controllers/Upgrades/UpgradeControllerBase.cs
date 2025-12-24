using Zenject;
using UnityEngine;
using System;

class UpgradeControllerBase : IInitializable
{
    protected UpgradeEnums _upgradeEnum;
    [Inject] protected CoreGameSignals _coreGameSignals { get; set; }
    [Inject] protected UpgradeSettings _upgrades{ get; set; }
    [Inject] protected PlayerSettings _playerSettings { get; set; }
    [Inject(Id = "PlayerDefaultSettings")] protected PlayerSettings _playerDefaultSettings { get; set; }
    [Inject] protected LoadGameDataCommand _loadCommand;
    [Inject] protected EnumCastCommand _enumCastCommand { get; set; }

    [Inject]
    private void Constructor()
    {
        InitUpgrades();
    }

    public virtual void Initialize()
    {
        SubscribeEvents();
    }

    private void InitUpgrades()
    {
        for (int i = 0; i < (int)UpgradeEnums.Count; i++)
        {
            _playerSettings.Settings[(UpgradeEnums)i] = 
                _loadCommand.CheckIfKeyInitialized(_enumCastCommand.StringToEnum<SaveDataEnums>(((UpgradeEnums)i).ToString())) ?
                _upgrades.Skills[(UpgradeEnums)i][_loadCommand.OnLoadGameData<int>(_enumCastCommand.StringToEnum<SaveDataEnums>(((UpgradeEnums)i).ToString())) - 1].UpgradeValue :
                _playerDefaultSettings.Settings[(UpgradeEnums)i];
        }
    }

    protected void OnUpgrade(UpgradeEnums upgradeEnum, int newLevel)
    {
        if (upgradeEnum == _upgradeEnum)
        {
            _playerSettings.Settings[upgradeEnum] = _upgrades.Skills[upgradeEnum][newLevel - 1].UpgradeValue;
        }

        _coreGameSignals.onUpgradePurchasedEnded?.Invoke(upgradeEnum);
    }

    protected void SubscribeEvents()
    {
        _coreGameSignals.onUpgradePurchased += OnUpgrade;
    }
}