using Zenject;
using UnityEngine;

class UpgradeControllerBase : IInitializable
{
    protected UpgradeEnums _upgradeEnum;
    [Inject] protected CoreGameSignals _coreGameSignals { get; set; }
    [Inject] protected UpgradeSettings _upgrades{ get; set; }
    [Inject] protected PlayerSettings _playerSettings { get; set; }

    public virtual void Initialize()
    {
        SubscribeEvents();
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