using UnityEngine;
using Zenject;

namespace Common.Installers
{
    public class Fighter2DSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _parentSpawnedElements;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(_parentSpawnedElements);
        }
    }
}