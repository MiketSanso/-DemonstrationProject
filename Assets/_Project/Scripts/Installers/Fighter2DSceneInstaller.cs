using GameScene.Level;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
    public class Fighter2DSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _parentSpawnedElements;

        [SerializeField]
        private CharactersFactory _charactersFactory;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(_parentSpawnedElements).AsSingle();
            Container.Bind<CharactersFactory>().FromInstance(_charactersFactory).AsSingle();
            Container.Bind<IInitializable>().To<EntryPoint>().AsSingle();
        }
    }
}