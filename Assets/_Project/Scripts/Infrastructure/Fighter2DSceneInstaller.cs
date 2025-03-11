using GameScene.Level;
using GameScene.Level.Texts;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
    public class Fighter2DSceneInstaller : MonoInstaller
    {
        [SerializeField] private CharactersFactory _charactersFactory;
        [SerializeField] private TextsRepository _textsRepository;

        public override void InstallBindings()
        {
            Container.Bind<CharactersFactory>().FromInstance(_charactersFactory).AsSingle();
            Container.Bind<IInitializable>().To<EntryPoint>().AsSingle();
            Container.Bind<TextsRepository>().FromInstance(_textsRepository).AsSingle();
        }
    }
}