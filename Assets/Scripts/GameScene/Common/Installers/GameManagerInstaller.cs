using GameScene.GameManager;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField]
        private GameManagement _gameManager;

        public override void InstallBindings()
        {
            Container.Bind<GameManagement>().FromInstance(_gameManager).AsSingle();
        }
    }
}