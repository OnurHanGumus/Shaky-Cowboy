using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

class MagazineCapacityUpgradeController : UpgradeControllerBase
{
    public override void Initialize()
    {
        base.Initialize();
        _upgradeEnum = UpgradeEnums.MagazineCapacityUpgrade;
    }
}