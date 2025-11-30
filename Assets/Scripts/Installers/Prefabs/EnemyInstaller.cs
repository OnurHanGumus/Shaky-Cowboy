using Data.MetaData;
using UnityEngine;
using Zenject;
using Signals;

namespace Installers.Prefabs
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField] private EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            Container.Bind<EnemySignals>().AsSingle();

            Container.BindInstance(_enemySettings).AsSingle();
        }
    }
}