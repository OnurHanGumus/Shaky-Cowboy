using Data.MetaData;
using UnityEngine;
using Zenject;
using Signals;

namespace Installers.Prefabs
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField] private EnemySettings _enemySettings;
        [SerializeField] private bool isAdditional = false;
        [Inject] private LoadGameDataCommand _loadCommand { get; set; }
        [Inject] private LevelSignals _levelSignals { get; set; }
        public override void InstallBindings()
        {
            Container.Bind<EnemySignals>().AsSingle();

            int levelId = _levelSignals.onGetLevelId();
            levelId = isAdditional ? levelId - 1 : levelId;

            EnemyModel model = new EnemyModel();
            model.Level = levelId;
            Container.BindInstance(model).AsSingle();

            _enemySettings = Resources.Load<EnemySettings>("Data/Enemies/" + levelId.ToString());
            Container.BindInstance(_enemySettings).AsSingle();
        }
    }
}