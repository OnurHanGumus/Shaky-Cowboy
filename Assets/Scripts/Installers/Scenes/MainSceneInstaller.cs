using Zenject;
using UnityEngine;
using Data.MetaData;
using Signals;

namespace Installers.Scenes
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject enemyPrefab;
        //[SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject tumbleweedPrefab;
        private BulletSettings _bulletSettings;
        private TumbleSettings _enemySpawnSettings;
        private TumbleweedSettings _tumblewoodSpawnSettings;
        private CoreGameSignals _coreGameSignals { get; set; }
        private LevelSignals _levelSignals { get; set; }
        [SerializeField] private int levelId = 1;
        public override void InstallBindings()
        {
            BindComponents();
            BindSettings();
        }

        void BindComponents()
        {
            _coreGameSignals = new CoreGameSignals();
            Container.BindInstance<CoreGameSignals>(_coreGameSignals).AsSingle();

            _levelSignals = new LevelSignals();
            Container.BindInstance<LevelSignals>(_levelSignals).AsSingle();

            //Container.Bind<CoreGameSignals>().AsSingle();
            Container.Bind<InputSignals>().AsSingle();
            //Container.Bind<LevelSignals>().AsSingle();
            Container.Bind<UISignals>().AsSingle();
            Container.Bind<ScoreSignals>().AsSingle();
            Container.Bind<SaveSignals>().AsSingle();
            Container.Bind<PoolSignals>().AsSingle();
            Container.Bind<AudioSignals>().AsSingle();
            Container.Bind<PlayerSignals>().AsSingle();
            Container.Bind<RevolverSignals>().AsSingle();

            Container.BindInterfacesAndSelfTo<TumbleweedSpawnManager>().AsSingle();

            Container.BindFactory<BulletManager, BulletManager.Factory>().FromComponentInNewPrefab(bulletPrefab);
            Container.BindFactory<EnemyManager, EnemyManager.Factory>().FromComponentInNewPrefab(enemyPrefab);
            //Container.BindFactory<ExplosionManager, ExplosionManager.Factory>().FromComponentInNewPrefab(explosionPrefab);
            Container.BindFactory<TumbleweedManager, TumbleweedManager.Factory>().FromComponentInNewPrefab(tumbleweedPrefab);

            Container.BindFactory<EpisodeManager, EpisodeManager.Factory>()
                .FromComponentInNewPrefabResource("Levels/" + 1.ToString());
            //Container.BindFactory<EpisodeManager, EpisodeManager.Factory>()
            //    .FromComponentInNewPrefabResource("Levels/" + 2.ToString());


        }

        private void BindSettings()
        {
            _bulletSettings = Resources.Load<BulletSettings>("Data/MetaData/BulletSettings");
            Container.BindInstance(_bulletSettings).AsSingle();

            _enemySpawnSettings = Resources.Load<TumbleSettings>("Data/MetaData/EnemySpawnSettings");
            Container.BindInstance(_enemySpawnSettings).AsSingle();

            _tumblewoodSpawnSettings = Resources.Load<TumbleweedSettings>("Data/MetaData/TumbleweedSettings");
            Container.BindInstance(_tumblewoodSpawnSettings).AsSingle();
        }
    }
}
