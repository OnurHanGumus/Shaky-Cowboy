using Data.MetaData;
using UnityEngine;
using Zenject;

namespace Installers.Prefabs
{
    public class DenemeInstaller : MonoInstaller<DenemeInstaller>
    {
        private IDeneme _denemeClass;

        public override void InstallBindings()
        {
            _denemeClass = new SecondClass();
            Container.BindInstance(_denemeClass);
        }
    }
}