using Zenject;
using UnityEngine;

class UpgradeControllerBase : IInitializable
{
    protected UpgradeEnums _upgradeEnum;
    protected int _currentLevel = 0;
    [Inject] protected CoreGameSignals _coreGameSignals { get; set; }
    [Inject] protected UpgradeSettings _upgrades{ get; set; }
    [Inject] protected PlayerSettings _playerSettings { get; set; }

    public virtual void Initialize()
    {
        SubscribeEvents();
    }

    protected void OnUpgrade(UpgradeEnums upgradeEnum)
    {
        if (upgradeEnum == _upgradeEnum)
        {
            _playerSettings.Settings[upgradeEnum] = _upgrades.Skills[upgradeEnum][_currentLevel].UpgradeValue;
        }
    }

    protected void SubscribeEvents()
    {
        _coreGameSignals.onUpgradePurchased += OnUpgrade;
    }
}