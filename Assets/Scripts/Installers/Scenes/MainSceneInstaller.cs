using Zenject;
using UnityEngine;
using Data.MetaData;
using Signals;

namespace Installers.Scenes
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        private BulletSettings _bulletSettings;
        private TumbleweedSpawnSettings _tumblewoodSpawnSettings;
        private CoreGameSignals _coreGameSignals { get; set; }
        private LevelSignals _levelSignals { get; set; }
        [SerializeField] private EnemyShootDelaySettings _shootDelaySettings;
        [SerializeField] private GameOptions _gameOptions;
        [SerializeField] private PlayerSettings _playerSettings;

        [SerializeField] private UpgradeSettings _healthSettings;
        [SerializeField] private UpgradeSettings _damageMultiplierSettings;
        [SerializeField] private UpgradeSettings _magazineCapacitySettings;
        [SerializeField] private UpgradeSettings _reloadSpeedSettings;
        public override void InstallBindings()
        {
            BindComponents();
            BindSettings();
        }

        void BindComponents()
        {
            _coreGameSignals = new CoreGameSignals();
            Container.BindInstance(_coreGameSignals).AsSingle();

            _levelSignals = new LevelSignals();
            Container.BindInstance(_levelSignals).AsSingle();
            Container.BindInstance(_shootDelaySettings).AsSingle();
            Container.BindInstance(_gameOptions).AsSingle();
            Container.BindInstance(_playerSettings).AsSingle();

            Container.BindInstance(_healthSettings).WithId("Health").AsTransient();
            Container.BindInstance(_damageMultiplierSettings).WithId("DamageMultiplier").AsTransient();
            Container.BindInstance(_magazineCapacitySettings).WithId("MagazineCapacity").AsTransient();
            Container.BindInstance(_reloadSpeedSettings).WithId("ReloadSpeed").AsTransient();

            Container.Bind<InputSignals>().AsSingle();
            Container.Bind<UISignals>().AsSingle();
            Container.Bind<ScoreSignals>().AsSingle();
            Container.Bind<SaveSignals>().AsSingle();
            Container.Bind<PoolSignals>().AsSingle();
            Container.Bind<AudioSignals>().AsSingle();
            Container.Bind<PlayerSignals>().AsSingle();
            Container.Bind<RevolverSignals>().AsSingle();

            Container.BindInterfacesAndSelfTo<TumbleweedSpawnManager>().AsSingle();
        }

        private void BindSettings()
        {
            _bulletSettings = Resources.Load<BulletSettings>("Data/MetaData/BulletSettings");
            Container.BindInstance(_bulletSettings).AsSingle();

            _tumblewoodSpawnSettings = Resources.Load<TumbleweedSpawnSettings>("Data/MetaData/TumbleweedSpawnSettings");
            Container.BindInstance(_tumblewoodSpawnSettings).AsSingle();
        }
    }
}
