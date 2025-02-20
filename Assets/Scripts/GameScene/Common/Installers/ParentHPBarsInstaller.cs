using UnityEngine;
using Zenject;

namespace Common.Installers
{
    public class ParentHPBarsInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _parentHPBars;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(_parentHPBars);
        }
    }
}