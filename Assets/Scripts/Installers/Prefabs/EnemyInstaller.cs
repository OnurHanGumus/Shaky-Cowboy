using Data.MetaData;
using UnityEngine;
using Zenject;
using Signals;

namespace Installers.Prefabs
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        private EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            Container.Bind<EnemySignals>().AsSingle();

            _enemySettings = Resources.Load<EnemySettings>("Data/MetaData/EnemySettings");

            Container.BindInstance(_enemySettings).AsSingle();
        }
    }
}