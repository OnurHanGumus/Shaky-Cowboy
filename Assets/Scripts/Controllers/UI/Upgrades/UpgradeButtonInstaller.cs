using Data.MetaData;
using UnityEngine;
using Zenject;

namespace Installers.Prefabs
{
    public class UpgradeButtonInstaller : MonoInstaller<UpgradeButtonInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<UpgradeButtonModel>().AsSingle();
            Container.Bind<UpgradeButtonInternalSignals>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeButtonController>().AsSingle();
        }
    }
}
