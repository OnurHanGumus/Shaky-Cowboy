using Data.MetaData;
using UnityEngine;
using Zenject;

namespace Installers.Prefabs
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        private PlayerSettings _playerSettings;
        
        public override void InstallBindings()
        {
            _playerSettings = Resources.Load<PlayerSettings>("Data/MetaData/PlayerSettings");

            Container.BindInstance(_playerSettings).AsSingle();
        }
    }
}
