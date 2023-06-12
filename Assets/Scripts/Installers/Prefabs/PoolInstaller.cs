using Data.MetaData;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Installers.Prefabs
{
    public class PoolInstaller : MonoInstaller<PoolInstaller>
    {
        [Inject] private BulletManager.Factory bulletFactory;
        [Inject] private TumbleweedManager.Factory tumbleweedFactory;
        [Inject] private Deneme.Factory denemeFactory;

        private List<IPool> factoryList;

        public override void InstallBindings()
        {
            factoryList = new List<IPool>();

            factoryList.Add(bulletFactory);
            factoryList.Add(tumbleweedFactory);
            factoryList.Add(denemeFactory);

            Container.BindInstance(factoryList);
        }
    }
}
